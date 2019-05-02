
using FluentAssertions;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using PdvChallenge.API.Infrastructure.DataContext;
using PdvChallenge.API.Infrastructure.Repositories;
using PdvChallenge.API.Model;
using System;
using System.Linq;
using System.IO;
using Xunit;
using System.Threading.Tasks;

namespace PdvChallenge.API.UnitTests.Infrastructure
{
    public class PdvRepository_Tests
    {
        private readonly IPdvRepository _pdvRepository;

        public PdvRepository_Tests()
        {
            var config = InitConfiguration();
            var services = new ServiceCollection();

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = config.GetConnectionString("DefaultConnection");
            }
            services.AddDbContext<PdvContext>(options =>
                options.UseNpgsql(
                    connectionString, opts => opts.UseNetTopologySuite()
                )
            );

            services.AddTransient<IPdvRepository, PdvRepository>();
            var provider = services.BuildServiceProvider();

            _pdvRepository = provider.GetService<IPdvRepository>();

        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }


        public class PdvRepository_Tests_Extension : PdvRepository_Tests
        {

            [Fact]
            public async void Given_InvalidParameters_ExpectException()
            {
                Func<Task> result = () => _pdvRepository.Get(-1);
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(result);

                result = () => _pdvRepository.Get(null);
                await Assert.ThrowsAsync<ArgumentNullException>(result);

                result = () => _pdvRepository.Add(null);
                await Assert.ThrowsAsync<ArgumentNullException>(result);

                result = () => _pdvRepository.Add(getInvalidPdv());
                await Assert.ThrowsAsync<ArgumentException>(result);

            }

            //Not so Unit Test
            [Fact]
            public async void Given_ValidParameters_NotExpectException()
            {
                var result = await Record.ExceptionAsync(() => _pdvRepository.List());
                Assert.IsNotType<Exception>(result);

                result = await Record.ExceptionAsync(() => _pdvRepository.Get(getValidPoint()));
                Assert.IsNotType<Exception>(result);

                result = await Record.ExceptionAsync(() => _pdvRepository.Get(25));
                Assert.IsNotType<Exception>(result);

                result = await Record.ExceptionAsync(() => _pdvRepository.Add(getValidPdv()));
                Assert.IsNotType<Exception>(result);

            }

        }

        private Point getValidPoint()
        {
            return new Point(1, 0);
        }

        private Pdv getInvalidPdv()
        {
            return new Pdv { id = -3, tradingName = "BluePuvb", ownerName = "Zxteste", address = new Point(1, 2), document = "asdfas115656", coverageArea = getValidCovarageArea() };
        }

        private Pdv getValidPdv()
        {
            return new Pdv { id = getNextId(), tradingName = "BluePuvb", ownerName = "Zxteste", address = new Point(1, 2), document = "asdfas115656", coverageArea = getValidCovarageArea()};
        }

        private int getMaxId()
        {
            return _pdvRepository.List().Result.Max(p => (p.id));
        }

        private int getNextId()
        {
            return _pdvRepository.List().Result.Max(p => (p.id)) + 1;
        }

        private MultiPolygon getValidCovarageArea()
        {

            Coordinate a = new Coordinate(0, 0);
            Coordinate b = new Coordinate(0, 1);
            Coordinate c = new Coordinate(1, 0);
            Coordinate d = new Coordinate(1, 1);
            Coordinate[] coords = { a, b, d, c, a };

            LinearRing lr = new LinearRing(coords);

            Polygon[] polygons = { new Polygon(lr) };

            return new MultiPolygon(polygons);
        }
    }
}

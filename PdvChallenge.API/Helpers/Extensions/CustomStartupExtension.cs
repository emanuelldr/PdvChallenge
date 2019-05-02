using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using PdvChallenge.API.Helpers.Defaults;
using PdvChallenge.API.Infrastructure.DataContext;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PdvChallenge.API.Helpers.Extensions
{
    public static class CustomStartupExtension
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            
            if(String.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            services.AddDbContext<PdvContext>(options =>
                options.UseNpgsql(
                    connectionString, opts => opts.UseNetTopologySuite()
                )
            );
            //services.AddDbContext<PdvContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection"), opts => opts.UseNetTopologySuite()));

            return services;
        }


        public static IServiceCollection AddCustomVersionedApiExplorer(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }



        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(
                opts =>
                {
                    using (var serviceProvider = services.BuildServiceProvider())
                    {
                        var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            opts.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                        }
                    }

                    opts.OperationFilter<SwaggerDefaultValues>();

                    opts.IncludeXmlComments(XmlCommentsFilePath);
                });

            return services;
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"PDV API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = ".NET Core Rest API with Postgre/Postgis, EFCore and Self-Documented with Swagger",
                Contact = new Contact() { Name = "EMANUEL ROCHA", Email = "" },
                TermsOfService = "Shareware",
                License = new License() { Name = "EMANUEL ROCHA V" + Assembly.GetExecutingAssembly().GetName().Version, Url = "https://github.com/ZXVentures/code-challenge/blob/master/backend.md" }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}

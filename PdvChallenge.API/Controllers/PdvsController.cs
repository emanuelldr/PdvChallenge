using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdvChallenge.API.Dto;
using PdvChallenge.API.Helpers.Mappers;
using PdvChallenge.API.Infrastructure.Repositories;
using GJson = GeoJSON.Net.Geometry;

namespace PdvChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdvsController : ControllerBase
    {

        IPdvRepository _repository;
        public PdvsController(IPdvRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Returns all PDVs in the database.
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<PdvDto>>> Get()
        {
            try
            {
                var pdvList = await _repository.List();

                if (pdvList == null || pdvList.Count == 0)
                    return NotFound();

                return PdvMapper.MapToDto(pdvList);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }


        /// <summary>
        /// Returns the PDV by a provided Id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PdvDto>> Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var pdv = await _repository.Get(id);

                if (pdv == null)
                    return NotFound();

                return PdvMapper.MapToDto(pdv);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Returns the nearest PDV, which covers the provided coordinates.
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        [HttpGet("{lng}/{lat}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<PdvDto>> Get(double lng, double lat)
        {
            try
            {
                var point = GeoMapper.ToNTSPoint(lng, lat);

                if (!point.IsValid || point.IsEmpty)
                    return BadRequest();

                var pdv = await _repository.Get(point);

                if (pdv == null)
                    return NotFound();

                return PdvMapper.MapToDto(pdv);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Insert a new PDV
        /// </summary>
        /// <param name="pdv"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post([FromBody] PdvDto pdv)
        {
            try
            {
                var pdvModel = PdvMapper.MapToModel(pdv);
                await _repository.Add(pdvModel);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}

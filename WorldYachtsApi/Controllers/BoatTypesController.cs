using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachts.Services.BoatType;
using WorldYachts.Services.Models;
using WorldYachtsApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/boats/types")]
    [ApiController]
    public class BoatTypesController : ControllerBase
    {
        private readonly IBoatTypeService _boatTypeService;
        private readonly IMapper _mapper;
        public BoatTypesController(IBoatTypeService boatTypeService, IMapper mapper)
        {
            _boatTypeService = boatTypeService;
            _mapper = mapper;
        }
        
        // GET: api/boats/types
        [HttpGet]
        public IActionResult Get()
        {
            var response = _boatTypeService.GetAll();
            return Ok(response);
        }

        // GET: api/boats/types/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _boatTypeService.GetById(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/boats/types
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] BoatTypeModel boatTypeModel)
        {
            var boatType = _mapper.Map<BoatType>(boatTypeModel);

            var response = await _boatTypeService.Add(boatType);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/boats/types/{id}
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] BoatTypeModel boatTypeModel)
        {
            var boatType = _mapper.Map<BoatType>(boatTypeModel);

            var response = await _boatTypeService.Update(id, boatType);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/boats/types/{id}
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _boatTypeService.Delete(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

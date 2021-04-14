using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachts.Services.BoatType;
using WorldYachts.Services.BoatWood;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/boats/woods")]
    [ApiController]
    public class BoatWoodsController : ControllerBase
    {
        private readonly IBoatWoodService _boatWoodService;
        private readonly IMapper _mapper;
        public BoatWoodsController(IBoatWoodService boatWoodService, IMapper mapper)
        {
            _boatWoodService = boatWoodService;
            _mapper = mapper;
        }

        // GET: api/boats/woods
        [HttpGet]
        public IActionResult Get()
        {
            var response = _boatWoodService.GetAll();
            return Ok(response);
        }

        // GET: api/boats/woods/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _boatWoodService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/boats/woods
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] BoatWoodModel boatWoodModel)
        {
            var boatWood = _mapper.Map<BoatWood>(boatWoodModel);

            var response = await _boatWoodService.AddAsync(boatWood);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/boats/woods/{id}
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] BoatWoodModel boatWoodModel)
        {
            var boatWood = _mapper.Map<BoatWood>(boatWoodModel);

            var response = await _boatWoodService.UpdateAsync(id, boatWood);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/boats/woods/{id}
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _boatWoodService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

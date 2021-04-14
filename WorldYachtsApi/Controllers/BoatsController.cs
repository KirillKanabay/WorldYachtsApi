using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Boat;
using WorldYachts.Services.BoatType;
using WorldYachts.Services.Models;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IBoatService _boatService;
        private readonly IMapper _mapper;
        public BoatsController(IBoatService boatService, IMapper mapper)
        {
            _boatService = boatService;
            _mapper = mapper;
        }

        #region Boat

        // GET: api/<BoatsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_boatService.GetAll());
        }

        // GET api/<BoatsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _boatService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<BoatsController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] BoatModel boatModel)
        {
            var boat = _mapper.Map<Boat>(boatModel);

            var response = await _boatService.AddAsync(boat);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/<BoatsController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] BoatModel boatModel)
        {
            var boat = _mapper.Map<Boat>(boatModel);

            var response = await _boatService.UpdateAsync(id, boat);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/<BoatsController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _boatService.DeleteAsync(id);
            
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        #endregion
        
    }
}

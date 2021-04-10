using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachts.Services.Boat;
using WorldYachts.Services.BoatType;
using WorldYachts.Services.Models;
using WorldYachtsApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoatsController : ControllerBase
    {
        private readonly IBoatService _boatService;
        private readonly IBoatTypeService _boatTypeService;
        public BoatsController(IBoatService boatService, IBoatTypeService boatTypeService)
        {
            _boatService = boatService;
            _boatTypeService = boatTypeService;
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
            var boat = await _boatService.GetById(id);
            if (boat == null)
            {
                return BadRequest("No record found against this id");
            }

            return Ok(boat);
        }

        // POST api/<BoatsController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] BoatModel boatModel)
        {
            var boat = await _boatService.Add(boatModel);

            if (boat == null)
            {
                return BadRequest("Can't add this record.");
            }

            return Ok(boat);
        }

        // PUT api/<BoatsController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] BoatModel boatModel)
        {
            var boat = await _boatService.Update(id, boatModel);

            if (boat == null)
            {
                return BadRequest("Can't update this record");
            }

            return Ok(boat);
        }

        // DELETE api/<BoatsController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var boat = await _boatService.Delete(id);
            if (boat == null)
            {
                return BadRequest("Can't delete this record.");
            }

            return Ok(boat);
        }

        #endregion
        
    }
}

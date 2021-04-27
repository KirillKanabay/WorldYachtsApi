using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachts.Services.AccessoryToBoat;
using WorldYachts.Services.BoatWood;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/accessories/fits")]
    [ApiController]
    public class AccessoryToBoatController : ControllerBase
    {
        private readonly IAccessoryToBoatService _accessoryToBoatService;
        private readonly IMapper _mapper;
        public AccessoryToBoatController(IAccessoryToBoatService accessoryToBoatService, IMapper mapper)
        {
            _accessoryToBoatService = accessoryToBoatService;
            _mapper = mapper;
        }

        // GET: api/accessories/compatibilities
        [HttpGet]
        public IActionResult Get()
        {
            var response = _accessoryToBoatService.GetAll();
            return Ok(response);
        }

        // GET: api/accessories/compatibilities/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _accessoryToBoatService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/accessories/compatibilities
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] AccessoryToBoatModel accessoryToBoatModel)
        {
            var accessoryToBoat = _mapper.Map<AccessoryToBoat>(accessoryToBoatModel);

            var response = await _accessoryToBoatService.AddAsync(accessoryToBoat);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/accessories/compatibilities/{id}
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] AccessoryToBoatModel accessoryToBoatModel)
        {
            var accessoryToBoat = _mapper.Map<AccessoryToBoat>(accessoryToBoatModel);

            var response = await _accessoryToBoatService.UpdateAsync(id, accessoryToBoat);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/accessories/compatibilities/{id}
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _accessoryToBoatService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

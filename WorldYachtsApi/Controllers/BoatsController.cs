using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Boats;
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
        private readonly ILogger<BoatsController> _logger;
        public BoatsController(IBoatService boatService, IMapper mapper, ILogger<BoatsController> logger)
        {
            _boatService = boatService;
            _mapper = mapper;
            _logger = logger;
        }

        #region Boat

        // GET: api/<BoatsController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all boats");
            return Ok(_boatService.GetAll());
        }

        // GET api/<BoatsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting boat by id:{id}");
            var response = await _boatService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<BoatsController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] Boat boat)
        {
            //var entity = _mapper.Map<Boat>(boat);
            _logger.LogInformation($"Adding boat(Name:{boat.Model})");
            var response = await _boatService.AddAsync(boat);

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/<BoatsController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] Boat boat)
        {
            _logger.LogInformation($"Updating boat (id:{id})");
            var response = await _boatService.UpdateAsync(id, boat);

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/<BoatsController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting boat (id:{id})");
            var response = await _boatService.DeleteAsync(id);
            
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        #endregion
        
    }
}

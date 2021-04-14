using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Accessories;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoriesController : ControllerBase
    {
        private readonly IAccessoryService _accessoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccessoriesController> _logger;
        public AccessoriesController(IAccessoryService accessoryService, IMapper mapper, ILogger<AccessoriesController> logger)
        {
            _accessoryService = accessoryService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<AccessoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all accessories");
            return Ok(_accessoryService.GetAll());
        }

        // GET api/<AccessoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting accessory by id:{id}");
            var response = await _accessoryService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<AccessoriesController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] AccessoryModel accessoryModel)
        {
            var accessory = _mapper.Map<Accessory>(accessoryModel);

            var response = await _accessoryService.AddAsync(accessory);
            _logger.LogInformation($"Adding accessory(Name:{accessory.Name}, Price: {accessory.Price})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/<AccessoriesController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] AccessoryModel accessoryModel)
        {
            var accessory = _mapper.Map<Accessory>(accessoryModel);

            var response = await _accessoryService.UpdateAsync(id, accessory);

            _logger.LogInformation($"Updating accessory (id:{id})");

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/<AccessoriesController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _accessoryService.DeleteAsync(id);
            _logger.LogInformation($"Deleting accessory (id:{id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

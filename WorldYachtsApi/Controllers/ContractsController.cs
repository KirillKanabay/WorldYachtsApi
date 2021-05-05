using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Accessories;
using WorldYachts.Services.Contracts;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController:ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContractsController> _logger;
        public ContractsController(IContractService contractService, IMapper mapper, ILogger<ContractsController> logger)
        {
            _contractService = contractService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<ContractsController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all contracts");
            return Ok(_contractService.GetAll());
        }

        // GET api/<ContractsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting contract by id:{id}");
            var response = await _contractService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<ContractsController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] Contract contract)
        {
            var response = await _contractService.AddAsync(contract);
            _logger.LogInformation($"Adding contract(Id:{contract.Id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/<ContractsController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] Contract contract)
        {
            var response = await _contractService.UpdateAsync(id, contract);

            _logger.LogInformation($"Updating contract (id:{id})");

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/<ContractsController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _contractService.DeleteAsync(id);
            _logger.LogInformation($"Deleting contract (id:{id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

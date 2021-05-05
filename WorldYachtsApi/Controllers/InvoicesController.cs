using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Contracts;
using WorldYachts.Services.Invoices;
using WorldYachtsApi.Helpers;

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController:ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IInvoiceService invoiceService, IMapper mapper, ILogger<InvoicesController> logger)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<InvoicesController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all invoices");
            return Ok(_invoiceService.GetAll());
        }

        // GET api/<InvoicesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting invoice by id:{id}");
            var response = await _invoiceService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<InvoicesController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] Invoice invoice)
        {
            var response = await _invoiceService.AddAsync(invoice);
            _logger.LogInformation($"Adding invoice (Id:{invoice.Id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/<InvoicesController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] Invoice invoice)
        {
            var response = await _invoiceService.UpdateAsync(id, invoice);

            _logger.LogInformation($"Updating invoice (id:{id})");

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/<InvoicesController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _invoiceService.DeleteAsync(id);
            _logger.LogInformation($"Deleting invoice (id:{id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

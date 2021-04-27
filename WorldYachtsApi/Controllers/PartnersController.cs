using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using WorldYachts.Data;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Partner;
using WorldYachtsApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;
        private readonly ILogger<PartnersController> _logger;

        public PartnersController(IPartnerService partnerService, IMapper mapper, ILogger<PartnersController> logger)
        {
            _partnerService = partnerService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<PartnerController>
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all partners");
            return Ok(_partnerService.GetAll());
        }

        //[HttpGet("[action]")]
        //public IActionResult PagePartners(int? pageNumber, int? pageSize)
        //{
        //    var partners = _dbContext.Partners;

        //    var currentPageNumber = pageNumber ?? 1;
        //    var currentPageSize = pageSize ?? 5;

        //    return Ok(partners.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        //}

        //[HttpGet("[action]")]
        //public IActionResult SearchPartners(string name)
        //{
        //    var partners = _dbContext.Partners.Where(p => p.Name.StartsWith(name));
        //    return Ok(partners);
        //}

        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting partner by id:{id}");
            var response = await _partnerService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<PartnerController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] Partner partner)
        {
            var entity = _mapper.Map<Partner>(partner);

            var response = await _partnerService.AddAsync(entity);
            _logger.LogInformation($"Adding partner(Name:{partner.Name})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/<PartnerController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] Partner partner)
        {
            var entity = _mapper.Map<Partner>(partner);

            var response = await _partnerService.UpdateAsync(id, entity);

            _logger.LogInformation($"Updating partner (id:{id})");

            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _partnerService.DeleteAsync(id);
            _logger.LogInformation($"Deleting partner (id:{id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
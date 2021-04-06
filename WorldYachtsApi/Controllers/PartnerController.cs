using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using WorldYachtsApi.Data;
using WorldYachtsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly WorldYachtsDbContext _dbContext;

        public PartnerController(WorldYachtsDbContext worldYachtsDbContext)
        {
            _dbContext = worldYachtsDbContext;
        }

        // GET: api/<PartnerController>
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Get(string sort)
        {
            IQueryable<Partner> partners;
            switch (sort)
            {
                case "desc":
                    partners = _dbContext.Partners.OrderByDescending(p => p.CreatedAt);
                    break;
                case "asc":
                    partners = _dbContext.Partners.OrderBy(p => p.CreatedAt);
                    break;
                default:
                    partners = _dbContext.Partners;
                    break;
            }

            return Ok(partners);
        }

        [HttpGet("[action]")]
        public IActionResult PagePartners(int? pageNumber, int? pageSize)
        {
            var partners = _dbContext.Partners;

            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;

            return Ok(partners.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public IActionResult SearchPartners(string name)
        {
            var partners = _dbContext.Partners.Where(p => p.Name.StartsWith(name));
            return Ok(partners);
        }
        
        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var partner =  _dbContext.Partners.Find(id);
            if (partner == null)
            {
                return NotFound("No record found against this id");
            }

            return Ok(partner);
        }

        // POST api/<PartnerController>
        [HttpPost]
        public IActionResult Post([FromBody] Partner partner)
        {
            var now = DateTime.UtcNow;
            
            partner.CreatedAt = now;
            
            _dbContext.Partners.Add(partner);
            _dbContext.SaveChanges();
            
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PartnerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Partner partner)
        {
            var entity = _dbContext.Partners.Find(id);
            if (entity == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                entity.Name = partner.Name;
                entity.Address = partner.Address;
                entity.City = partner.City;

                _dbContext.SaveChanges();

                return Ok("Record updated successfully");
            }
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var partner = _dbContext.Partners.Find(id);

            if (partner == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {
                _dbContext.Partners.Remove(partner);
                _dbContext.SaveChanges();
                return Ok("Partner deleted...");
            }
            
        }
    }
}

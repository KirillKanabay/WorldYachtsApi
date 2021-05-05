using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WorldYachts.Data.Entities;
using WorldYachts.Services.SalesPersons;
using WorldYachts.Services.Users;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;


namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonsController : ControllerBase
    {
        private readonly ISalesPersonService _salesPersonService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<SalesPersonsController> _logger;

        public SalesPersonsController(ISalesPersonService salesPersonService,
            IMapper mapper, 
            ILogger<SalesPersonsController> logger,
            IUserService userService)
        {
            _salesPersonService = salesPersonService;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        // GET: api/<SalesPersonController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all sales persons");
            return Ok(_salesPersonService.GetAll());
        }

        // GET api/<SalesPersonController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting sales person by id:{id}");
            var response = await _salesPersonService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<SalesPersonController>
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Post([FromBody] SalesPersonModel salesPersonModel)
        {
            var salesPerson = _mapper.Map<SalesPerson>(salesPersonModel);
            var user = _mapper.Map<User>(salesPersonModel);

            //Если данные менеджера или пользовательские данные(Почта, логин) уже существуют возвращаем ошибку 
            if (await _userService.IsIdenticalEntityAsync(user))
            {
                return BadRequest("An user with such data already exists");
            }

            //Добавляем данные менеджера в БД
            _logger.LogInformation($"Adding sales person(First name:{salesPerson.FirstName}, Second name: {salesPerson.SecondName})");
            var salesPersonResponse = await _salesPersonService.AddAsync(salesPerson);
            if (!salesPersonResponse.IsSuccess)
            {
                _logger.LogError(salesPersonResponse.Message);
                return BadRequest(salesPersonResponse.Message);
            }

            //Добавляем пользователя в БД
            user.UserId = salesPersonResponse.Data.Id;
            var userRegisterResponse = await _userService.AddAsync(user);

            if (!userRegisterResponse.IsSuccess)
            {
                await _salesPersonService.DeleteAsync(salesPersonResponse.Data.Id);
                _logger.LogError(userRegisterResponse.Message);
                return BadRequest(userRegisterResponse.Message);
            }

            return Ok(salesPersonResponse.Data);
        }

        // PUT api/<SalesPersonController>/5
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] SalesPerson salesPerson)
        {
            var sp = _mapper.Map<SalesPerson>(salesPerson);   
            //Обновляем данные менеджера в БД
            _logger.LogInformation($"Update sales person(First name:{salesPerson.FirstName}, Second name: {salesPerson.SecondName})");
            
            var salesPersonResponse = await _salesPersonService.UpdateAsync(id, sp);
            
            if (!salesPersonResponse.IsSuccess)
            {
                _logger.LogError(salesPersonResponse.Message);
                return BadRequest(salesPersonResponse.Message);
            }

            return Ok(salesPersonResponse.Data);
        }

        // DELETE api/<SalesPersonController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _salesPersonService.DeleteAsync(id);
            _logger.LogInformation($"Deleting sales person (id:{id})");
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            var user = _userService.GetAll().FirstOrDefault(u => u.UserId == id);
            var userResponse = await _userService.DeleteAsync(user.Id);

            return Ok(response.Data);
        }
    }
}

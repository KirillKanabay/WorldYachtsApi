using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Customers;
using WorldYachts.Services.Users;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

namespace WorldYachtsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService, IMapper mapper, IUserService userService, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }

        // GET: api/<CustomersController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Getting all customers");
            return Ok(_customerService.GetAll());
        }

        // GET api/<CustomersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting customer by id:{id}");
            var response = await _customerService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                _logger.LogError(response.Message);
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/<CustomersController>
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] CustomerModel customerModel)
        {
            var customer = _mapper.Map<Customer>(customerModel);
            var user = _mapper.Map<User>(customerModel);

            //Если данные клиента или пользовательские данные(Почта, логин) уже существуют возвращаем ошибку 
            if (await _customerService.IsIdenticalEntityAsync(customer) 
                || await _userService.IsIdenticalEntityAsync(user))
            {
                return BadRequest("An user with such data already exists");
            }

            //Добавляем данные клиента в БД
            _logger.LogInformation($"Adding customer(First name:{customer.FirstName}, Second name: {customer.SecondName})");
            var customerResponse = await _customerService.AddAsync(customer);
            if (!customerResponse.IsSuccess)
            {
                _logger.LogError(customerResponse.Message);
                return BadRequest(customerResponse.Message);
            }

            //Добавляем пользователя в БД
            user.UserId = customerResponse.Data.Id;
            var userRegisterResponse = await _userService.AddAsync(user);

            if (!userRegisterResponse.IsSuccess)
            {
                await _customerService.DeleteAsync(customerResponse.Data.Id);
                _logger.LogError(userRegisterResponse.Message);
                return BadRequest(userRegisterResponse.Message);
            }

            return Ok(customerResponse.Data);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            //Обновляем данные клиента в БД
            _logger.LogInformation($"Update customer (First name:{customerEntity.FirstName}, Second name: {customerEntity.SecondName})");

            var customerResponse = await _customerService.UpdateAsync(id, customer);

            if (!customerResponse.IsSuccess)
            {
                _logger.LogError(customerResponse.Message);
                return BadRequest(customerResponse.Message);
            }

            return Ok(customerResponse.Data);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _customerService.DeleteAsync(id);
            _logger.LogInformation($"Deleting customer (id:{id})");
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

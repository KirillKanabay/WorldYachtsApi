using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorldYachts.Data.Entities;
using WorldYachts.Services.Admin;
using WorldYachts.Services.Customer;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.SalesPerson;
using WorldYachts.Services.User;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;


namespace WorldYachtsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISalesPersonService _salesPersonService;
        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, 
            ISalesPersonService salesPersonService, 
            ICustomerService customerService,
            IAdminService adminService,
            IMapper mapper)
        {
            _userService = userService;
            _salesPersonService = salesPersonService;
            _customerService = customerService;
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.AuthenticateAsync(model);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        #region Register

        [HttpPost("register/customer")]
        public async Task<IActionResult> Register(CustomerModel customerModel)
        {
            //Получаем сущность клиента и пользователя через модель клиента
            var customer = _mapper.Map<Customer>(customerModel);
            var user = _mapper.Map<User>(customerModel);

            //Если данные клиента или пользовательские данные(Почта, логин) уже существуют возвращаем ошибку 
            if (await _customerService.IsIdenticalEntityAsync(customer)
                || await _userService.IsIdenticalEntityAsync(user))
            {
                return BadRequest("An user with such data already exists");
            }

            //Добавляем данные клиента в БД
            var customerResponse = await _customerService.AddAsync(customer);
            if (!customerResponse.IsSuccess)
            {
                return BadRequest(customerResponse.Message);
            }

            //Добавляем пользователя в БД
            user.UserId = customerResponse.Data.Id;
            var userRegisterResponse = await _userService.AddAsync(user);

            if (!userRegisterResponse.IsSuccess)
            {
                return BadRequest(userRegisterResponse.Message);
            }

            //Сразу аутентифицируем зарегистрированного пользователя
            return await Authenticate(new AuthenticateRequest()
            {
                Username = userRegisterResponse.Data.Username,
                Password = userRegisterResponse.Data.Password
            });
        }

        [HttpPost("register/salesperson")]
        [Authorize("Admin")]
        public async Task<IActionResult> Register(SalesPersonModel salesPersonModel)
        {
            //Получаем сущность менеджера и пользователя через модель клиента
            var salesPerson = _mapper.Map<SalesPerson>(salesPersonModel);
            var user = _mapper.Map<User>(salesPersonModel);

            //Если данные менеджера или пользовательские данные(Почта, логин) уже существуют возвращаем ошибку 
            if (await _userService.IsIdenticalEntityAsync(user))
            {
                return BadRequest("An user with such data already exists");
            }

            //Добавляем данные менеджера в БД
            var salesPersonResponse = await _salesPersonService.AddAsync(salesPerson);
            if (!salesPersonResponse.IsSuccess)
            {
                return BadRequest(salesPersonResponse.Message);
            }

            //Добавляем пользователя в БД
            user.UserId = salesPersonResponse.Data.Id;
            var userRegisterResponse = await _userService.AddAsync(user);

            if (!userRegisterResponse.IsSuccess)
            {
                return BadRequest(userRegisterResponse.Message);
            }

            return Ok(salesPersonResponse.Data);
        }
        #endregion

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userResponse = await _userService.GetByIdAsync(id);
            
            if (!userResponse.IsSuccess)
            {
                return NotFound(userResponse.Message);
            }

            var user = userResponse.Data;

            switch (user.Role)
            {
                case "Customer":
                    var customer = await _customerService.GetByIdAsync(user.UserId);
                    return Ok(customer.Data);
                case "Sales Person":
                    var sp = await _salesPersonService.GetByIdAsync(user.UserId);
                    return Ok(sp.Data);
                case "Admin":
                    var admin = await _adminService.GetByIdAsync(user.UserId);
                    return Ok(admin.Data);
                default:
                    return NotFound("No record found against this id");
            }
        }
        

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var salesPersons = _salesPersonService.GetAll().ToList();
            var admins = _adminService.GetAll().ToList();
            var customers = _customerService.GetAll().ToList();

            List<object> users = new List<object>()
            {
                salesPersons, admins, customers
            };

            return Ok(users);
        }
    }
}

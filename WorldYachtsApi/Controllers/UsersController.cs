using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WorldYachts.Services.Admin;
using WorldYachts.Services.Customer;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.SalesPerson;
using WorldYachts.Services.User;
using WorldYachtsApi.Helpers;


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

        public UsersController(IUserService userService, 
            ISalesPersonService salesPersonService, 
            ICustomerService customerService,
            IAdminService adminService)
        {
            _userService = userService;
            _salesPersonService = salesPersonService;
            _customerService = customerService;
            _adminService = adminService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        #region Register

        [HttpPost("register/customer")]
        public async Task<IActionResult> Register(CustomerModel customerModel)
        {
            var response = await _customerService.Register(customerModel);

            if (response == null)
            {
                return BadRequest(new { message = "Didn't register!" });
            }

            return Ok(response);
        }

        [HttpPost("register/salesperson")]
        [Authorize("Admin")]
        public async Task<IActionResult> Register(SalesPersonModel salesPersonModel)
        {
            var response = await _salesPersonService.Register(salesPersonModel);

            if (response == null)
            {
                return BadRequest(new { message = "Didn't register!" });
            }

            return Ok(response);
        }
        #endregion

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = _userService.GetById(id);
            
            if (user == null)
            {
                return NotFound("No record found against this id");
            }

            switch (user.Role)
            {
                case "Customer":
                    return Ok(await _customerService.GetById(user.UserId));
                case "Sales Person":
                    return Ok(await _salesPersonService.GetById(user.UserId));
                case "Admin":
                    return Ok(await _adminService.GetById(user.UserId));
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

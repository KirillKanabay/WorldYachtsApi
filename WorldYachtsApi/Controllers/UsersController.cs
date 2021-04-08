using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        public UsersController(IUserService userService, ISalesPersonService salesPersonService, ICustomerService customerService)
        {
            _userService = userService;
            _salesPersonService = salesPersonService;
            _customerService = customerService;
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

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}

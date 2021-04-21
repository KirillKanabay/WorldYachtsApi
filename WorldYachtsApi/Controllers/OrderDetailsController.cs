using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data.Entities;
using WorldYachts.Services.OrderDetails;
using WorldYachtsApi.Helpers;
using WorldYachtsApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorldYachtsApi.Controllers
{
    [Route("api/orders/details")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;
        public OrderDetailsController(IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderDetailService = orderDetailService;
            _mapper = mapper;
        }

        // GET: api/orders/details
        [HttpGet]
        public IActionResult Get()
        {
            var response = _orderDetailService.GetAll();
            return Ok(response);
        }

        // GET: api/orders/details/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _orderDetailService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // POST api/orders/details
        [HttpPost]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Post([FromBody] OrderDetailModel orderDetailModel)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);

            var response = await _orderDetailService.AddAsync(orderDetail);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // PUT api/boats/types/{id}
        [HttpPut("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDetailModel orderDetailModel)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailModel);

            var response = await _orderDetailService.UpdateAsync(id, orderDetail);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        // DELETE api/boats/types/{id}
        [HttpDelete("{id}")]
        [Authorize("Admin", "Sales Person")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderDetailService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }
    }
}

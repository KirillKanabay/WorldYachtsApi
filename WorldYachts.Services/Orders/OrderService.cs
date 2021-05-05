using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.Orders
{
    public class OrderService:IOrderService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;

        public OrderService(WorldYachtsDbContext worldYachtsDbContext, IMapper mapper)
        {
            _db = worldYachtsDbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Data.Entities.Order>> AddAsync(Data.Entities.Order order)
        {
            var now = DateTime.UtcNow;
            try
            {
                var addedOrder = (await _db.Orders.AddAsync(order)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Data.Entities.Order>()
                {
                    IsSuccess = true,
                    Data = addedOrder,
                    Message = $"Order (id:{order.Id}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Order>()
                {
                    IsSuccess = false,
                    Data = order,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.Order>> GetByIdAsync(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Boat)
                .Include(o => o.Customer)
                .Include(o => o.SalesPerson)
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(a => a.Id == id);

            return new ServiceResponse<Data.Entities.Order>()
            {
                IsSuccess = order != null,
                Data = order,
                Message = order != null ? $"Got order by id: {id}" : "Order not found",
                Time = DateTime.UtcNow,
            };
        }

        public IEnumerable<Data.Entities.Order> GetAll()
        {
            var orders = _db.Orders
                .Include(o => o.Boat)
                .Include(o => o.Customer)
                .Include(o => o.SalesPerson)
                .Include(o => o.OrderDetails);

            return orders;
        }

        public async Task<ServiceResponse<Data.Entities.Order>> UpdateAsync(int id, Data.Entities.Order order)
        {
            var now = DateTime.UtcNow;
            try
            {
                var updatedOrder = await _db.Orders.FindAsync(id);
                updatedOrder= _mapper.Map(order, updatedOrder);
                updatedOrder.Id = id;

                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Order>()
                {
                    IsSuccess = true,
                    Data = updatedOrder,
                    Message = $"Order (id:{order.Id}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Order>()
                {
                    IsSuccess = false,
                    Data = order,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.Order>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var order = await _db.Orders.FindAsync(id);

                var deletedOrder = _db.Orders.Remove(order).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Order>()
                {
                    IsSuccess = deletedOrder != null,
                    Data = deletedOrder,
                    Message = deletedOrder != null
                        ? $"Order (id:{id}) deleted" : "Order not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Order>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }
    }
}

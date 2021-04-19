using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.OrderDetails
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;

        public OrderDetailService(WorldYachtsDbContext worldYachtsDbContext, IMapper mapper)
        {
            _db = worldYachtsDbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<OrderDetail>> AddAsync(OrderDetail orderDetail)
        {
            var now = DateTime.UtcNow;
            try
            {
                var addedOrderDetail = (await _db.OrderDetails.AddAsync(orderDetail)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Data.Entities.OrderDetail>()
                {
                    IsSuccess = true,
                    Data = addedOrderDetail,
                    Message = $"OrderDetail (id:{orderDetail.Id}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.OrderDetail>()
                {
                    IsSuccess = false,
                    Data = orderDetail,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<OrderDetail>> GetByIdAsync(int id)
        {
            var orderDetail = await _db.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Accessory)
                .FirstOrDefaultAsync(a => a.Id == id);

            return new ServiceResponse<Data.Entities.OrderDetail>()
            {
                IsSuccess = orderDetail != null,
                Data = orderDetail,
                Message = orderDetail != null ? $"Got order detail by id: {id}" : "Order detail not found",
                Time = DateTime.UtcNow,
            };
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            var orderDetails = _db.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Accessory);
            
            return orderDetails;
        }

        public async Task<ServiceResponse<OrderDetail>> UpdateAsync(int id, OrderDetail orderDetail)
        {
            var now = DateTime.UtcNow;
            try
            {
                var updatedOrderDetail = await _db.OrderDetails.FindAsync(id);
                updatedOrderDetail = _mapper.Map(orderDetail, updatedOrderDetail);
                updatedOrderDetail.Id = id;

                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.OrderDetail>()
                {
                    IsSuccess = true,
                    Data = updatedOrderDetail,
                    Message = $"Order detail (id:{orderDetail.Id}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.OrderDetail>()
                {
                    IsSuccess = false,
                    Data = orderDetail,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<OrderDetail>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var orderDetail = await _db.OrderDetails.FindAsync(id);

                var deletedOrderDetail = _db.OrderDetails.Remove(orderDetail).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.OrderDetail>()
                {
                    IsSuccess = deletedOrderDetail != null,
                    Data = deletedOrderDetail,
                    Message = deletedOrderDetail != null
                        ? $"Order detail (id:{id}) deleted" : "Order detail not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.OrderDetail>()
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

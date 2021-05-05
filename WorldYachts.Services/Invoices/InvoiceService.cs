using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Invoices
{
    public class InvoiceService:IInvoiceService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;

        public InvoiceService(WorldYachtsDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Invoice>> AddAsync(Invoice invoice)
        {
            var now = DateTime.UtcNow;
            try
            {
                var addedInvoice = (await _db.Invoices.AddAsync(invoice)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Invoice>()
                {
                    IsSuccess = true,
                    Data = addedInvoice,
                    Message = $"Invoice (id:{invoice.Id}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Invoice>()
                {
                    IsSuccess = false,
                    Data = invoice,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Invoice>> GetByIdAsync(int id)
        {
            var invoice = await _db.Invoices
                .Include(i => i.Contract)
                .FirstOrDefaultAsync(i => i.Id == id);

            return new ServiceResponse<Invoice>()
            {
                IsSuccess = invoice != null,
                Data = invoice,
                Message = invoice != null ? $"Got invoice by id: {id}" : "Invoice not found",
                Time = DateTime.UtcNow,
            };
        }

        public IEnumerable<Invoice> GetAll()
        {
            var invoices = _db.Invoices
                .Include(i => i.Contract);

            return invoices;
        }

        public async Task<ServiceResponse<Invoice>> UpdateAsync(int id, Invoice invoice)
        {
            var now = DateTime.UtcNow;
            try
            {
                var updatedInvoice = await _db.Invoices.FindAsync(id);
                updatedInvoice = _mapper.Map(invoice, updatedInvoice);
                updatedInvoice.Id = id;

                await _db.SaveChangesAsync();

                return new ServiceResponse<Invoice>()
                {
                    IsSuccess = true,
                    Data = updatedInvoice,
                    Message = $"Invoice (id:{invoice.Id}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Invoice>()
                {
                    IsSuccess = false,
                    Data = invoice,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Invoice>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var invoice = await _db.Invoices.FindAsync(id);

                var deletedInvoice = _db.Invoices.Remove(invoice).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Invoice>
                {
                    IsSuccess = deletedInvoice != null,
                    Data = deletedInvoice,
                    Message = deletedInvoice != null
                        ? $"Invoice (id:{id}) deleted" : "Invoice not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Invoice>()
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

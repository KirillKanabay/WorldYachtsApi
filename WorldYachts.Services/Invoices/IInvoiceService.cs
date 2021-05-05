using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Invoices
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<Invoice>> AddAsync(Invoice invoice);
        Task<ServiceResponse<Invoice>> GetByIdAsync(int id);
        IEnumerable<Invoice> GetAll();
        Task<ServiceResponse<Invoice>> UpdateAsync(int id, Invoice invoice);
        Task<ServiceResponse<Invoice>> DeleteAsync(int id);
    }
}

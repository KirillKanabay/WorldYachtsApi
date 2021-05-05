using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Admins
{
    public interface IAdminService
    {
        IEnumerable<Admin> GetAll();
        Task<ServiceResponse<Admin>> GetByIdAsync(int id);
    }
}

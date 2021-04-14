using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Admin
{
    public interface IAdminService
    {
        IEnumerable<Data.Entities.Admin> GetAll();
        Task<ServiceResponse<Data.Entities.Admin>> GetByIdAsync(int id);
    }
}

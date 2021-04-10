using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Admin
{
    public interface IAdminService
    {
        IEnumerable<Data.Entities.Admin> GetAll();
        Task<Data.Entities.Admin> GetById(int id);
    }
}

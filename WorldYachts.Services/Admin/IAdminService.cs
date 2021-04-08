using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Admin
{
    public interface IAdminService
    {
        IEnumerable<WorldYachts.Data.Models.Admin> GetAll();
        Task<Data.Models.Admin> GetById(int id);
    }
}

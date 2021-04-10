using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;

namespace WorldYachts.Services.Admin
{
    public class AdminService:IAdminService
    {
        private readonly IEfRepository<Data.Entities.Admin> _repository;
        public AdminService(IEfRepository<Data.Entities.Admin> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Data.Entities.Admin> GetAll()
        {
            return _repository.GetAll();
        }
        public async Task<ServiceResponse<Data.Entities.Admin>> GetById(int id)
        {
            var admin = await _repository.GetById(id);

            return new ServiceResponse<Data.Entities.Admin>()
            {
                IsSuccess = admin != null,
                Data = admin,
                Message = admin != null ? $"Got admin by id: {id}" : "Admin not found",
                Time = DateTime.UtcNow,
            };
        }
    }
}

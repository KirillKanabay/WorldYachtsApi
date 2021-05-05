using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Admins
{
    public class AdminService:IAdminService
    {
        private readonly IEfRepository<Admin> _repository;
        public AdminService(IEfRepository<Admin> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Admin> GetAll()
        {
            return _repository.GetAll();
        }
        public async Task<ServiceResponse<Admin>> GetByIdAsync(int id)
        {
            var admin = await _repository.GetById(id);

            return new ServiceResponse<Admin>()
            {
                IsSuccess = admin != null,
                Data = admin,
                Message = admin != null ? $"Got admin by id: {id}" : "Admin not found",
                Time = DateTime.UtcNow,
            };
        }
    }
}

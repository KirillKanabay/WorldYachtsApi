using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.Partners
{
    public class PartnerService:IPartnerService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;
        public PartnerService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Data.Entities.Partner>> AddAsync(Data.Entities.Partner partner)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(partner))
                {
                    return new ServiceResponse<Data.Entities.Partner>()
                    {
                        IsSuccess = false,
                        Data = partner,
                        Message = $"Partner (Name:{partner.Name}) already exist.",
                        Time = now
                    };
                }

                var addedPartner = (await _db.Partners.AddAsync(partner)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Data.Entities.Partner>()
                {
                    IsSuccess = true,
                    Data = addedPartner,
                    Message = $"Partner (id:{partner.Id} Name:{partner.Name}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Partner>()
                {
                    IsSuccess = false,
                    Data = partner,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public IEnumerable<Data.Entities.Partner> GetAll()
        {
            return _db.Partners
                .Include(p => p.Accessories);
        }

        public async Task<ServiceResponse<Data.Entities.Partner>> GetByIdAsync(int id)
        {
            var partner = await _db.Partners
                .Include(p => p.Accessories)
                .FirstOrDefaultAsync(b => b.Id == id);

            return new ServiceResponse<Data.Entities.Partner>()
            {
                IsSuccess = partner != null,
                Data = partner,
                Message = partner != null ? $"Got partner by id: {id}" : "Partner not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.Partner>> UpdateAsync(int id, Data.Entities.Partner partner)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(partner))
                {
                    return new ServiceResponse<Data.Entities.Partner>()
                    {
                        IsSuccess = false,
                        Data = partner,
                        Message = $"Partner (Name:{partner.Name}) already exist.",
                        Time = now
                    };
                }

                var updatedPartner = await _db.Partners.FindAsync(id);
                updatedPartner = _mapper.Map(partner, updatedPartner);
                _db.Partners.Update(updatedPartner);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Partner>()
                {
                    IsSuccess = true,
                    Data = updatedPartner,
                    Message = $"Partner (id:{updatedPartner.Id} Name:{partner.Name}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Partner>()
                {
                    IsSuccess = false,
                    Data = partner,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.Partner>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var partner = await _db.Partners.FindAsync(id);

                var deletedPartner = _db.Partners.Remove(partner).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Partner>()
                {
                    IsSuccess = deletedPartner != null,
                    Data = deletedPartner,
                    Message = deletedPartner != null ? $"Partner (id:{id}, Name:{deletedPartner.Name}) deleted" 
                        : "Partner not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Partner>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.Partner partner)
        {
            if (await _db.Partners.FirstOrDefaultAsync(p => p.Name == partner.Name
                                                            && p.Id != partner.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}

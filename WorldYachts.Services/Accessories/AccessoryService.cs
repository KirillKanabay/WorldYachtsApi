using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Accessories
{
    public class AccessoryService:IAccessoryService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;
        public AccessoryService(WorldYachtsDbContext worldYachtsDbContext, IMapper mapper)
        {
            _db = worldYachtsDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Accessory>> AddAsync(Accessory accessory)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(accessory))
                {
                    return new ServiceResponse<Accessory>()
                    {
                        IsSuccess = false,
                        Data = accessory,
                        Message = $"Accessory (Model:{accessory.Name}) already exist.",
                        Time = now
                    };
                }

                var addedAccessory = (await _db.Accessories.AddAsync(accessory)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Accessory>()
                {
                    IsSuccess = true,
                    Data = addedAccessory,
                    Message = $"Accessory (id:{accessory.Id} Name:{accessory.Name}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Accessory>()
                {
                    IsSuccess = false,
                    Data = accessory,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Accessory>> GetByIdAsync(int id)
        {
            var accessory = await _db.Accessories
                .Include(a => a.Partner)
                .Include(a => a.AccessoryToBoat)
                .FirstOrDefaultAsync(a => a.Id == id);

            return new ServiceResponse<Accessory>()
            {
                IsSuccess = accessory != null,
                Data = accessory,
                Message = accessory != null ? $"Got accessory by id: {id}" : "Accessory not found",
                Time = DateTime.UtcNow,
            };
        }

        public IEnumerable<Accessory> GetAll()
        {
            return _db.Accessories
                .Include(a => a.Partner)
                .Include(a => a.AccessoryToBoat)
                .OrderBy(a => a.Name);
        }

        public async Task<ServiceResponse<Accessory>> UpdateAsync(int id, Accessory accessory)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(accessory))
                {
                    return new ServiceResponse<Accessory>()
                    {
                        IsSuccess = false,
                        Data = accessory,
                        Message = $"Accessory (Name:{accessory.Name}) already exist.",
                        Time = now
                    };
                }

                var updatedAccessory = await _db.Accessories.FindAsync(id);
                updatedAccessory = _mapper.Map(accessory, updatedAccessory);
                updatedAccessory.Id = id;

                await _db.SaveChangesAsync();

                return new ServiceResponse<Accessory>()
                {
                    IsSuccess = true,
                    Data = updatedAccessory,
                    Message = $"Accessory (id:{updatedAccessory.Id} Name:{updatedAccessory.Name}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Accessory>()
                {
                    IsSuccess = false,
                    Data = accessory,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Accessory>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var accessory = await _db.Accessories.FindAsync(id);

                var deletedAccessory = _db.Accessories.Remove(accessory).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Accessory>()
                {
                    IsSuccess = deletedAccessory != null,
                    Data = deletedAccessory,
                    Message = deletedAccessory != null 
                        ? $"Accessory (id:{id}, Name:{deletedAccessory.Name}) deleted" : "Accessory not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Accessory>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<bool> IsIdenticalEntityAsync(Accessory accessory)
        {
            if (await _db.Accessories.FirstOrDefaultAsync(a => a.Name == accessory.Name
                                                               && a.PartnerId == accessory.PartnerId
                                                               && a.Id != accessory.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}

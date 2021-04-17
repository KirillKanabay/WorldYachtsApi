using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.AccessoryToBoat
{
    public class AccessoryToBoatService:IAccessoryToBoatService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;
        public AccessoryToBoatService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Data.Entities.AccessoryToBoat>> AddAsync(Data.Entities.AccessoryToBoat accessoryToBoat)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(accessoryToBoat))
                {
                    return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                    {
                        IsSuccess = false,
                        Data = accessoryToBoat,
                        Message = $"Сompatibility accessory to boat (Boat id:{accessoryToBoat.BoatId} " +
                                  $"Accessory id: {accessoryToBoat.AccessoryId}) already exist.",
                        Time = now
                    };
                }

                var addedAccessoryToBoat = (await _db.AccessoryToBoats.AddAsync(accessoryToBoat)).Entity;
                
                await _db.SaveChangesAsync();
                
                return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                {
                    IsSuccess = true,
                    Data = addedAccessoryToBoat,
                    Message = $"Compatibility accessory to boat (Boat id:{addedAccessoryToBoat.BoatId}" +
                              $" Accessory id:{addedAccessoryToBoat.AccessoryId}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                {
                    IsSuccess = false,
                    Data = accessoryToBoat,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public IEnumerable<Data.Entities.AccessoryToBoat> GetAll()
        {
            return _db.AccessoryToBoats
                .Include(atb => atb.Accessory)
                .Include(atb => atb.Boat);
        }

        public async Task<ServiceResponse<Data.Entities.AccessoryToBoat>> GetByIdAsync(int id)
        {
            var accessoryToBoat = await _db.AccessoryToBoats
                    .Include(atb => atb.Accessory)
                    .Include(atb => atb.Boat)
                    .FirstOrDefaultAsync(atb=>atb.Id == id);
            
            return new ServiceResponse<Data.Entities.AccessoryToBoat>()
            {
                IsSuccess = accessoryToBoat != null,
                Data = accessoryToBoat,
                Message = accessoryToBoat != null ? $"Got compatibility accessory to boat by id: {id}" 
                    : "Compatibility accessory to boat not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.AccessoryToBoat>> UpdateAsync(int id, Data.Entities.AccessoryToBoat accessoryToBoat)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(accessoryToBoat))
                {
                    return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                    {
                        IsSuccess = false,
                        Data = accessoryToBoat,
                        Message = $"Сompatibility accessory to boat (Boat id:{accessoryToBoat.BoatId} " +
                                  $"Accessory id: {accessoryToBoat.AccessoryId}) already exist.",
                        Time = now
                    };
                }

                var updatedAccessoryToBoat = await _db.AccessoryToBoats.FindAsync(id);
                updatedAccessoryToBoat = _mapper.Map(accessoryToBoat, updatedAccessoryToBoat);

                _db.AccessoryToBoats.Update(updatedAccessoryToBoat);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                {
                    IsSuccess = true,
                    Data = updatedAccessoryToBoat,
                    Message = $"Compatibility accessory to boat (Boat id:{updatedAccessoryToBoat.BoatId}" +
                              $" Accessory id:{updatedAccessoryToBoat.AccessoryId}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                {
                    IsSuccess = false,
                    Data = accessoryToBoat,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.AccessoryToBoat>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var accessoryToBoat = await _db.AccessoryToBoats.FindAsync(id);

                var deletedAccessoryToBoat = _db.AccessoryToBoats.Remove(accessoryToBoat).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                {
                    IsSuccess = deletedAccessoryToBoat != null,
                    Data = deletedAccessoryToBoat,
                    Message = deletedAccessoryToBoat != null
                        ? $"Compatibility accessory to boat (id:{id}, BoatId: {accessoryToBoat.BoatId})" +
                          $" AccessoryId: {accessoryToBoat.AccessoryId} deleted" : "Compatibility accessory to boat not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.AccessoryToBoat>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.AccessoryToBoat accessoryToBoat)
        {
            if (await _db.AccessoryToBoats.FirstOrDefaultAsync(atb => atb.AccessoryId == accessoryToBoat.AccessoryId
                                                                      && atb.BoatId == accessoryToBoat.BoatId
                                                              && atb.Id != accessoryToBoat.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}

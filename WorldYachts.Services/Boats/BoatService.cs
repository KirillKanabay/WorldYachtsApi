using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Boats
{
    public class BoatService : IBoatService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;
        public BoatService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Boat>> AddAsync(Boat boat)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(boat))
                {
                    return new ServiceResponse<Boat>()
                    {
                        IsSuccess = false,
                        Data = boat,
                        Message = $"Boat (Model:{boat.Model}) already exist.",
                        Time = now
                    };
                }

                var addedBoat = (await _db.Boats.AddAsync(boat)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Boat>()
                {
                    IsSuccess = true,
                    Data = addedBoat,
                    Message = $"Boat (id:{addedBoat.Id} Model:{addedBoat.Model}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Boat>()
                {
                    IsSuccess = false,
                    Data = boat,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public IEnumerable<Boat> GetAll()
        {
            return _db.Boats
                .Include(b => b.BoatType)
                .Include(b => b.BoatWood)
                .OrderBy(b => b.Model);
        }

        public async Task<ServiceResponse<Boat>> GetByIdAsync(int id)
        {
            var boat = await _db.Boats
                .Include(b=>b.BoatWood)
                .Include(b=>b.BoatType)
                .FirstOrDefaultAsync(b=>b.Id == id);
            
            return new ServiceResponse<Boat>()
            {
                IsSuccess = boat != null,
                Data = boat,
                Message = boat != null ? $"Got boat by id: {id}" : "Boat not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Boat>> UpdateAsync(int id, Boat boat)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(boat))
                {
                    return new ServiceResponse<Boat>()
                    {
                        IsSuccess = false,
                        Data = boat,
                        Message = $"Boat (Model:{boat.Model}) already exist.",
                        Time = now
                    };
                }

                var updatedBoat = await _db.Boats.FindAsync(id);
                updatedBoat = _mapper.Map(boat, updatedBoat);
                _db.Boats.Update(updatedBoat);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Boat>()
                {
                    IsSuccess = true,
                    Data = updatedBoat,
                    Message = $"Boat (id:{updatedBoat.Id} Model:{updatedBoat.Model}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Boat>()
                {
                    IsSuccess = false,
                    Data = boat,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
            
        }

        public async Task<ServiceResponse<Boat>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var boat = await _db.Boats.FindAsync(id);

                var deletedBoat = _db.Boats.Remove(boat).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Boat>()
                {
                    IsSuccess = deletedBoat != null,
                    Data = deletedBoat,
                    Message = deletedBoat != null ? $"Boat (id:{id}, Model:{deletedBoat.Model}) deleted" : "Boat not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Boat>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<bool> IsIdenticalEntityAsync(Boat boat)
        {
            if (await _db.Boats.FirstOrDefaultAsync(b => b.Model == boat.Model && b.Id != boat.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}
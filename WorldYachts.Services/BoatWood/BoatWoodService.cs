using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.BoatWood
{
    public class BoatWoodService:IBoatWoodService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;
        public BoatWoodService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Data.Entities.BoatWood>> AddAsync(Data.Entities.BoatWood boatWood)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(boatWood))
                {
                    return new ServiceResponse<Data.Entities.BoatWood>()
                    {
                        IsSuccess = false,
                        Data = boatWood,
                        Message = $"Boat wood (Wood:{boatWood.Wood}) already exist.",
                        Time = now
                    };
                }

                var addedBoatWood = (await _db.BoatWoods.AddAsync(boatWood)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Data.Entities.BoatWood>()
                {
                    IsSuccess = true,
                    Data = addedBoatWood,
                    Message = $"Boat wood (id:{addedBoatWood.Id} Wood:{addedBoatWood.Wood}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.BoatWood>()
                {
                    IsSuccess = false,
                    Data = boatWood,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public IEnumerable<Data.Entities.BoatWood> GetAll()
        {
            return _db.BoatWoods;
        }

        public async Task<ServiceResponse<Data.Entities.BoatWood>> GetByIdAsync(int id)
        {
            var  boatWood = await _db.BoatWoods.FindAsync(id);

            return new ServiceResponse<Data.Entities.BoatWood>()
            {
                IsSuccess = boatWood != null,
                Data = boatWood,
                Message = boatWood != null ? $"Got boat wood by id: {id}" : "Boat wood not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.BoatWood>> UpdateAsync(int id, Data.Entities.BoatWood boatWood)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(boatWood))
                {
                    return new ServiceResponse<Data.Entities.BoatWood>()
                    {
                        IsSuccess = false,
                        Data = boatWood,
                        Message = $"Boat wood (Wood:{boatWood.Wood}) already exist.",
                        Time = now
                    };
                }

                var updatedBoatWood = await _db.BoatWoods.FindAsync(id);
                updatedBoatWood = _mapper.Map(boatWood, updatedBoatWood);
                
                _db.BoatWoods.Update(updatedBoatWood);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.BoatWood>()
                {
                    IsSuccess = true,
                    Data = updatedBoatWood,
                    Message = $"Boat wood (id:{updatedBoatWood.Id} Wood:{updatedBoatWood.Wood}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.BoatWood>()
                {
                    IsSuccess = false,
                    Data = boatWood,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.BoatWood>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var boatWood = await _db.BoatWoods.FindAsync(id);

                var deletedBoatWood = _db.BoatWoods.Remove(boatWood).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.BoatWood>()
                {
                    IsSuccess = deletedBoatWood != null,
                    Data = deletedBoatWood,
                    Message = deletedBoatWood != null
                        ? $"Boat wood (id:{id}, Wood: {deletedBoatWood.Wood}) deleted" : "Boat wood not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.BoatWood>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.BoatWood boatWood)
        {
            if (await _db.BoatWoods.FirstOrDefaultAsync(bw => bw.Wood == boatWood.Wood
                                                             && bw.Id != boatWood.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}

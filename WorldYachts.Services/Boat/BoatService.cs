using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services.Models;

namespace WorldYachts.Services.Boat
{
    public class BoatService : IBoatService
    {
        private readonly WorldYachtsDbContext _dbContext;
        private readonly IMapper _mapper;

        public BoatService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Data.Entities.Boat> Add(BoatModel boatModel)
        {
            var boat = _mapper.Map<Data.Entities.Boat>(boatModel);
            
            if (await IsIdenticalEntity(boat))
            {
                return null;
            }

            var addedBoat = await _dbContext.Boats.AddAsync(boat);

            await _dbContext.SaveChangesAsync();

            return addedBoat.Entity;
        }

        public IEnumerable<Data.Entities.Boat> GetAll()
        {
            return _dbContext.Boats
                .Include(boat => boat.BoatWood)
                .Include(boat => boat.BoatType)
                .Include(boat => boat.AccessoryToBoat);
        }

        public async Task<Data.Entities.Boat> GetById(int id)
        {
            var boat = await _dbContext.Boats.FindAsync(id);
            return boat;
        }

        public async Task<Data.Entities.Boat> Update(int id, BoatModel boatModel)
        {
            var boat = await _dbContext.Boats.FindAsync(id);

            if (boat == null)
            {
                return null;
            }

            _mapper.Map(boatModel, boat);
            
            if (await IsIdenticalEntity(boat))
            {
                return null;
            }

            await _dbContext.SaveChangesAsync();

            return boat;
        }

        public async Task<Data.Entities.Boat> Delete(int id)
        {
            var boat = await GetById(id);
            if (boat == null)
            {
                return null;
            }

            var deletedBoat = _dbContext.Boats.Remove(boat);
            await _dbContext.SaveChangesAsync();

            return deletedBoat.Entity;
        }

        public async Task<bool> IsIdenticalEntity(Data.Entities.Boat boat)
        {
            if (await _dbContext.Boats.AnyAsync(b => b.Model.ToLower() == boat.Model.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
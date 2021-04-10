using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IEfRepository<Data.Entities.Boat> _repository;

        public BoatService(IEfRepository<Data.Entities.Boat> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<Data.Entities.Boat>> Add(Data.Entities.Boat boat)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntity(boat))
            {
                return new ServiceResponse<Data.Entities.Boat>()
                {
                    IsSuccess = false,
                    Data = boat,
                    Message = $"Boat (Model:{boat.Model}) already exist.",
                    Time = now
                };
            }

            var addedBoat = await _repository.Add(boat);

            return new ServiceResponse<Data.Entities.Boat>()
            {
                IsSuccess = true,
                Data = addedBoat,
                Message = $"Boat (id:{addedBoat.Id} Model:{addedBoat.Model}) added",
                Time = now
            };
        }

        public IEnumerable<Data.Entities.Boat> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<ServiceResponse<Data.Entities.Boat>> GetById(int id)
        {
            var boat = await _repository.GetById(id);

            return new ServiceResponse<Data.Entities.Boat>()
            {
                IsSuccess = boat != null,
                Data = boat,
                Message = boat != null ? $"Got boat by id: {id}" : "Boat type not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.Boat>> Update(int id, Data.Entities.Boat boat)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntity(boat))
            {
                return new ServiceResponse<Data.Entities.Boat>()
                {
                    IsSuccess = false,
                    Data = boat,
                    Message = $"Boat (Model:{boat.Model}) already exist.",
                    Time = now
                };
            }

            var updatedBoat = await _repository.Update(id, boat);
            
            return new ServiceResponse<Data.Entities.Boat>()
            {
                IsSuccess = true,
                Data = updatedBoat,
                Message = $"Boat (id:{updatedBoat.Id} Model:{updatedBoat.Model}) updated",
                Time = now
            };
        }

        public async Task<ServiceResponse<Data.Entities.Boat>> Delete(int id)
        {
            var now = DateTime.UtcNow;
            var deletedBoat = await _repository.Delete(id);

            return new ServiceResponse<Data.Entities.Boat>()
            {
                IsSuccess = deletedBoat != null,
                Data = deletedBoat,
                Message = deletedBoat != null ? $"Boat (id:{id}, Model:{deletedBoat.Model}) deleted" : "Boat not found",
                Time = now,
            };
        }

        public async Task<bool> IsIdenticalEntity(Data.Entities.Boat boat)
        {
            if (await _repository.Find(b => b.Model == boat.Model && b.Id != boat.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}
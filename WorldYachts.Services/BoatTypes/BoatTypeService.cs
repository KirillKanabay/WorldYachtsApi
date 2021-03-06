using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.BoatTypes
{
    public class BoatTypeService:IBoatTypeService
    {
        private readonly IEfRepository<Data.Entities.BoatType> _repository;
        
        public BoatTypeService(IEfRepository<Data.Entities.BoatType> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<Data.Entities.BoatType>> AddAsync(Data.Entities.BoatType boatType)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntityAsync(boatType))
            {
                return new ServiceResponse<Data.Entities.BoatType>()
                {
                    IsSuccess = false,
                    Data = boatType,
                    Message = $"Boat type ({boatType.Type}) already exists.",
                    Time = now
                };
            }

            var addedBoatType = await _repository.Add(boatType);
            
            return new ServiceResponse<Data.Entities.BoatType>()
            {
                IsSuccess = true,
                Data = addedBoatType,
                Message = $"New boat (Id:{addedBoatType.Type}) type added",
                Time = now
            };
        }

        public IEnumerable<Data.Entities.BoatType> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<ServiceResponse<Data.Entities.BoatType>> GetByIdAsync(int id)
        {
            var boatType = await _repository.GetById(id);

            return new ServiceResponse<Data.Entities.BoatType>()
            {
                IsSuccess = boatType != null,
                Data = boatType,
                Message = boatType != null ? $"Got boat type by id: {id}" : "Boat type not found",
                Time = DateTime.UtcNow,
            };

        }

        public async Task<ServiceResponse<Data.Entities.BoatType>> UpdateAsync(int id, Data.Entities.BoatType boatType)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntityAsync(boatType))
            {
                return new ServiceResponse<Data.Entities.BoatType>()
                {
                    IsSuccess = false,
                    Data = boatType,
                    Message = $"Boat type ({boatType.Type}) already exists.",
                    Time = now
                };
            }

            var updatedBoatType = await _repository.Update(id, boatType);

            return new ServiceResponse<Data.Entities.BoatType>()
            {
                IsSuccess = true,
                Data = updatedBoatType,
                Message = $"New boat (Id:{updatedBoatType.Type}) type added",
                Time = now
            };
        }

        public async Task<ServiceResponse<Data.Entities.BoatType>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            var deletedBoatType = await _repository.Delete(id);

            return new ServiceResponse<Data.Entities.BoatType>()
            {
                IsSuccess = deletedBoatType != null,
                Data = deletedBoatType,
                Message = deletedBoatType != null ? $"Boat type (id:{id}, Type:{deletedBoatType.Type}) deleted" : "Boat type not found",
                Time = now,
            };
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.BoatType boatType)
        {
            if (await _repository.Find(bt => bt.Type == boatType.Type && bt.Id != boatType.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}

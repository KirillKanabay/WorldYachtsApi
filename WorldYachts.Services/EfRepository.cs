using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services
{
    public class EfRepository<TEntity> : IEfRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly WorldYachtsDbContext _dbContext;
        private readonly IMapper _mapper;

        public EfRepository(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var addedEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            
            await _dbContext.SaveChangesAsync();

            return addedEntity.Entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            return result;
        }

        public async Task<TEntity> Update(int id, TEntity entity)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            result = _mapper.Map(entity, result);
            result.Id = id;

            await _dbContext.SaveChangesAsync();
            
            return result;
        }

        public async Task<TEntity> Delete(int id)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            var deletedEntity = _dbContext.Remove(result);
            
            await _dbContext.SaveChangesAsync();
            
            return deletedEntity.Entity;
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> filterFunc)
        {
            var result = _dbContext.Set<TEntity>().Where(filterFunc);
            return result;
        }

        public async Task<TEntity> Find(Func<TEntity, bool> findFunc)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(findFunc);
            return result;
        }
    }
}

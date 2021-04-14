using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services
{
    public interface IEfRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add(TEntity entity);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Update(int id, TEntity entity);
        Task<TEntity> Delete(int id);
        IEnumerable<TEntity> Filter(Func<TEntity, bool> filterAction);
        Task<TEntity> Find(Func<TEntity, bool> findFunc);
    }
}

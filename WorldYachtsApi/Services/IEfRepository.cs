using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachtsApi.Entity;

namespace WorldYachtsApi.Services
{
    public interface IEfRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Получить записи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(long id);

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<long> Add(TEntity entity);
    }
}

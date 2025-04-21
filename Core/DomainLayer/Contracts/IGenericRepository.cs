using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task AddAsync (TEntity entity);

        void Update (TEntity entity);

        void Remove (TEntity entity);

        Task <TEntity?> GetByIdAsync(Tkey Id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        #region With  Specifications    

        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications);
        #endregion
    }
}

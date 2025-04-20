using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorice
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositoris = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typeName = typeof(TEntity).Name;


            if(_repositoris.TryGetValue(typeName,out object? value)) 
                return (IGenericRepository<TEntity , Tkey>)value;



            else
            {
                var Repo = new GenericRepositorice<TEntity,Tkey>(_dbContext);
                _repositoris["typeName"]= Repo;
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}

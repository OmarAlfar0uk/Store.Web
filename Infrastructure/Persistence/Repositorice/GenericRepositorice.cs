using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorice
{
    public class GenericRepositorice<TEntity, Tkey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);
      

        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await _dbContext.Set<TEntity>().ToListAsync();
     

        public async Task<TEntity?> GetByIdAsync(Tkey Id) => await _dbContext.Set<TEntity>().FindAsync(Id); 
       

        public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity); 

        public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
    }
}

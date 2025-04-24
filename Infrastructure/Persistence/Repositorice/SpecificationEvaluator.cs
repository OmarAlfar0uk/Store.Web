using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorice
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQurery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> specifications ) where  TEntity  : BaseEntity<TKey>
        {
            var Query = InputQuery;
            if (specifications.Criateria is not null) 
            {
                 Query = Query.Where(specifications.Criateria);
            }

            if(specifications.OrderBy is not null)
            {
                Query.OrderBy(specifications.OrderBy);
            }

            if (specifications is not null)
            {
                Query.OrderByDescending(specifications.OrderByDescending);  
            }

            if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
             Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery , IncludeExp) => CurrentQuery.Include(IncludeExp));      
            }

            if (specifications.IsPaginated)
            {
                Query = Query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return Query;
        }
    }
}

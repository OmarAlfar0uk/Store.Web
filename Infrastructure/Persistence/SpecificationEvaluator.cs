using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
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

            if(specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
             Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery , IncludeExp) => CurrentQuery.Include(IncludeExp));      
            }
            return Query;
        }
    }
}

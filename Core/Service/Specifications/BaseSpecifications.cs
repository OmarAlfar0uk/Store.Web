using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    abstract internal class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriateriaExpression)
        {
            Criateria = CriateriaExpression;    
        }

        public Expression<Func<TEntity, bool>>? Criateria { get; private set; }

        #region Iclude
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];



        protected void AddInclode(Expression<Func<TEntity, object>> includeExpressions)
         =>  IncludeExpressions.Add(includeExpressions);
        #endregion

        #region Sortion
        public Expression<Func<TEntity, object>> OrderBy  { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending  { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderBy = orderByExp;

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp) => OrderByDescending = orderByDescExp;
    #endregion

}

}

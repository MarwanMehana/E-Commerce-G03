using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificatioinEvaluator
    {
        //create query 
        // _dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)
        public static IQueryable<TEnitiy> CreateQuery<TEnitiy, Tkey>(IQueryable<TEnitiy> InputQuery, ISpecification<TEnitiy, Tkey> specification) where TEnitiy : BaseEntity<Tkey>
        {
            //_dbContext.Products
            var query = InputQuery;

            if (specification.Criteria is not null)
            {
                // _dbContext.Products.Where(P=>P.Id==id)
                query = query.Where(specification.Criteria);
            }
            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IsPaginated)
            {
                query = query.Skip(specification.Skip);
                query = query.Take(specification.Take);
            }

            if (specification.IncludeExpressions is not null && specification.IncludeExpressions.Any())
            {
                //foreach (var expression in specification.IncludeExpressions)
                //{
                //    query = query.Include(expression);
                //}

                query = specification.IncludeExpressions.Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }

            return query;

        }
    }
}

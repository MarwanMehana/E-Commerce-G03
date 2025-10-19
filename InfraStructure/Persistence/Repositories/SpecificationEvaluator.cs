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
        public static IQueryable<TEnitiy> CreateQuery<TEnitiy, Tkey>(IQueryable<TEnitiy> InputQuery, ISpecification<TEnitiy, Tkey> specification) where TEnitiy : BaseEntity<Tkey>
        {
            var query = InputQuery;

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
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

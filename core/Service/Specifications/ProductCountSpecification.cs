using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId)
                     && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
                     && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
        }
    }
}

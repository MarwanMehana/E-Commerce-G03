using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTranferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")] // baseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get All Products
        [HttpGet]
        // GET: baseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        // Get Product by Id
        [HttpGet("{id:int}")]
        // GET: baseUrl/api/Products/10
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // Get All Brands
        [HttpGet("brands")]
        // GET: baseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        // Get All Types
        [HttpGet("types")]
        // GET: baseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}

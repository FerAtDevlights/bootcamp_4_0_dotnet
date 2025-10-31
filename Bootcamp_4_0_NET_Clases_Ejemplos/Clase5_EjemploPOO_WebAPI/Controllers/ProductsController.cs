using Clase5_EjemploPOO_WebAPI.Models;

using Microsoft.AspNetCore.Mvc;

namespace Clase5_EjemploPOO_WebAPI.Controllers
{
    [Route("api/[controller]")] // api/products
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> _products = new()
        {
            new Product(1, "Producto A", 100),
            new Product(2, "Producto B", 200),
            new Product(3, "Producto C", 300)
        };

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(_products); //200 OK
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(); //404 Not Found
            return Ok(product); //200 OK
        }
    }
}

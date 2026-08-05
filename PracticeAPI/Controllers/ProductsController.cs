using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Data;
using PracticeAPI.Entities;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(StoreService context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProducts(int id)
        {
            var product = context.Products.Find(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}
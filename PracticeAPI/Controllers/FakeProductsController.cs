using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Data;
using PracticeAPI.Entities;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FakeProductsController(FakeStoreService service) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return service.Products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProducts(int id)
        {
            // List<T>.Find takes a predicate, unlike DbSet<T>.Find which takes key values
            var product = service.Products.Find(p => p.Id == id);

            if (product == null) return NotFound();

            return product;
        }
    }
}

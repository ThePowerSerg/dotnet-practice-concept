using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Services;
using PracticeAPI.Entities;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(StoreService serviceContext) : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return serviceContext.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProducts(int id)
        {
            var product = serviceContext.Products.Find(id);

            if (product == null) return NotFound();

            return product;
        }
    }
}
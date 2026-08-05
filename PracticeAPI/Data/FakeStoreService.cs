using PracticeAPI.Entities;

namespace PracticeAPI.Data
{
    // Fake in-memory service - mirrors the shape of StoreContext (a Products
    // collection you can query) but without touching a real database.
    // Handy for practicing DI registration/lifetimes without EF Core in the mix.
    public class FakeStoreService
    {
        public List<Product> Products { get; } = new()
        {
            new Product
            {
                Id = 1,
                Name = "Fake Board One",
                Description = "A placeholder product served from an in-memory list.",
                Price = 10000,
                PictureURL = "/images/products/fake1.png",
                Brand = "FakeBrand",
                Type = "Boards",
                QuantityInStock = 50
            },
            new Product
            {
                Id = 2,
                Name = "Fake Hat Two",
                Description = "Another placeholder product served from an in-memory list.",
                Price = 2500,
                PictureURL = "/images/products/fake2.png",
                Brand = "FakeBrand",
                Type = "Hats",
                QuantityInStock = 75
            },
            new Product
            {
                Id = 3,
                Name = "Fake Boots Three",
                Description = "A third placeholder product served from an in-memory list.",
                Price = 18000,
                PictureURL = "/images/products/fake3.png",
                Brand = "FakeBrand",
                Type = "Boots",
                QuantityInStock = 30
            }
        };
    }
}

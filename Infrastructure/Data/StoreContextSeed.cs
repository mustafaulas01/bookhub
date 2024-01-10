

using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public  class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context)
        {         
         if(!context.Publishers.Any())
         {
            var publishersData=File.ReadAllText("../Infrastructure/Data/SeedData/publisher.json");
            var publishers=JsonSerializer.Deserialize<List<Publisher>>(publishersData);
            context.Publishers.AddRange(publishers);

         }
         if(!context.Categories.Any())
         {
            var categoriesData=File.ReadAllText("../Infrastructure/Data/SeedData/category.json");
            var categories=JsonSerializer.Deserialize<List<Category>>(categoriesData);
            context.Categories.AddRange(categories);

         }
         if(!context.Products.Any())
         {
            var productsData=File.ReadAllText("../Infrastructure/Data/SeedData/product.json");
            var products=JsonSerializer.Deserialize<List<Product>>(productsData);
            context.Products.AddRange(products);

         }

         if(context.ChangeTracker.HasChanges())
         {
            await context.SaveChangesAsync();
         }
       
        }
        

    }
}
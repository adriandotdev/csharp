using Microsoft.EntityFrameworkCore;

namespace Route {

    public class ProductRoute {

        public static void Map(WebApplication app) {
            var products = app.MapGroup("/api/v1/products");

            products.MapGet("/", GetProducts);

            products.MapPost("/", CreateProduct);

            products.MapDelete("/{id}", DeleteTodo);

            products.MapPut("/{id}", UpdateProduct);
        }

        private static async Task<IResult> DeleteTodo(int id, ProductDb db)
        {
                var product = await db.Products.FindAsync(id);

            if (product is null) {
                return TypedResults.NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            
            return TypedResults.Ok();
        }

       private static async Task<IResult> GetProducts(ProductDb db, int pageSize = 10, int pageNumber = 1) {

           var products = await db.Products.Select(product => new Product() {Id = product.Id, Name = product.Name, Price = product.Price, CreatedAt = product.CreatedAt, Category = product.Category}).OrderBy(p => p.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
           var totalProducts = db.Products.Count();

           return TypedResults.Ok(new {
             products,
             totalProducts
           });
        }

       private static async Task<IResult> CreateProduct(Product product, ProductDb db) {
                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return TypedResults.Ok(new {
                    message = "Product added successfully",
                    product
                });
        }

       private static async Task<IResult> UpdateProduct(int id, Product product, ProductDb db) {
            var productToUpdate = await db.Products.FindAsync(id);

            if (productToUpdate is null) {
                return TypedResults.NotFound(new {
                    message = "Product not found"
                });
            }

            productToUpdate.Name = product.Name ?? productToUpdate.Name;
            productToUpdate.Price = product.Price != 0 ? product.Price : productToUpdate.Price;
            productToUpdate.CategoryId = product.CategoryId != 0 ? product.CategoryId : productToUpdate.CategoryId;

            await db.SaveChangesAsync();

            return TypedResults.Ok(new {
                message = "Product updated successfully",
                product = productToUpdate
            });
        }
    }
}
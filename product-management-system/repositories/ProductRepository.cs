
public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository () : base() {
    }
    public void AddProduct(Product product)
    {
        if (product == null) throw new Exception("Product is required");

        this.productManagementContext.Products.Add(product);

        this.productManagementContext.SaveChanges();
    }

    public List<Category> GetCategories()
    {
        return this.productManagementContext.Categories.ToList();
    }

    public dynamic GetProducts(int pageNumber = 1, int pageSize = 10) {

        var products = this.productManagementContext.Products
            .OrderBy(product => product.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var totalProducts = this.productManagementContext.Products.Count();

        return new {
            products,
            totalProducts
        };
    }

    public bool DeleteProductById(int id)
    {
        var productToDelete = productManagementContext.Products.FirstOrDefault(product => product.Id == id);

        if (productToDelete != null) {
            this.productManagementContext.Products.Remove(productToDelete);
            this.productManagementContext.SaveChanges();

            return true;
        }

        return false;
    }
}
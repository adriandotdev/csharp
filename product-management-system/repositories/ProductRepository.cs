
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

    public List<Product> GetProducts(int pageNumber = 1, int pageSize = 10) {
        return this.productManagementContext.Products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
}
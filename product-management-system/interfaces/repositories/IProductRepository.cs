public interface IProductRepository {

    public void AddProduct(Product product);
    public List<Category> GetCategories();

    public List<Product> GetProducts(int pageNumber = 1, int pageSize = 10);
}
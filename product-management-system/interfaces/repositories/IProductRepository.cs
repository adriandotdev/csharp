public interface IProductRepository {

    public void AddProduct(Product product);
    public List<Category> GetCategories();

    public dynamic GetProducts(int pageNumber = 1, int pageSize = 10);

    public bool DeleteProductById(int id);
}
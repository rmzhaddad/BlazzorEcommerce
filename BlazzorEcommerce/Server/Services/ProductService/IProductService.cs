namespace BlazzorEcommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string SearchText,int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string SearchText);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
    }
}

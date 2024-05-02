﻿namespace BlazzorEcommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Product> products { get; set; }
        Task GetProducts(string? categoryUrl=null);
        Task<ServiceResponse<Product>> GetProduct(int productId);
    }
}

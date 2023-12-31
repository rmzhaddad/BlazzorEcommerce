﻿using System.Net.Http.Json;

namespace BlazzorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> products { get; set; }=new List<Product>();

        public async Task GetProducts()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            if(result!=null&& result.Data!=null)
            {
            products = result.Data;
            }
        }

        async Task<ServiceResponse<Product>> IProductService.GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }
    }
}

namespace BlazzorEcommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>()
            {
                Data = await _context.Products.ToListAsync()
            };
            return response;
        }

        async Task<ServiceResponse<Product>> IProductService.GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product= await _context.Products.FindAsync(productId);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry,but this porduct does not exist";
            }
            else
            {
                response.Data = product;
            }
            return response;
        }
    }
}

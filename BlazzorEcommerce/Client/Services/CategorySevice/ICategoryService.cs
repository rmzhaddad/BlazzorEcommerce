namespace BlazzorEcommerce.Client.Services.CategorySevice
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }
        Task GetCategories();
    }
}

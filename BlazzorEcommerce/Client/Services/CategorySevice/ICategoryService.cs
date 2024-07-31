namespace BlazzorEcommerce.Client.Services.CategorySevice
{
    public interface ICategoryService
    {
        event Action OnChange;
        List<Category> Categories { get; set; }
        List<Category> AdminCategories { get; set; }
        Task GetCategories();
        Task GetAdminCategories();
        Task AddCategory(Category category);
        Task DeleteCategory(int cattegoryid);
        Task UpdateCategory(Category category);
        Category CreateNewCategory();
    }
}

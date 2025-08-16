namespace DotBudget.Data.Services
{
    public interface ICategoryService
    {
        public Category GetCategory(string id);
        public Category AddCategory(Category category);
        public void DeleteCategory(string id);
        public Category UpdateCategory(Category updatedCategory);
    }
}

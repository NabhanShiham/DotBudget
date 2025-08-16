using Microsoft.EntityFrameworkCore;

namespace DotBudget.Data.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly ApplicationDbContext context;
        public CategoryService(ApplicationDbContext dbcontext)
        {
            context = dbcontext;
        }

        public Category AddCategory(Category category)
        {
            if (category == null) {
                throw new ArgumentNullException(nameof(category));
            }
            try
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return category;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return category;
            }
        }

        public void DeleteCategory(string id)
        {
            context.Categories.Remove(GetCategory(id));
            context.SaveChanges();
        }

        public Category GetCategory(string id)
        {
            if (string.IsNullOrEmpty(id)) { 
                throw new ArgumentNullException(nameof(id));
            }
            var category = context.Categories.Include(p => p.ApplicationUser).FirstOrDefault(p => p.Id == id);
            return category ?? throw new KeyNotFoundException($"Category with ID {id} not found!");
        }

        public Category UpdateCategory(Category updatedCategory)
        {
            context.Categories.Update(updatedCategory);
            context.SaveChanges();
            return updatedCategory;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Runtime;

namespace DotBudget.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Categories = new HashSet<Category>();
            Receipts = new HashSet<Receipt>();
        }
        public Profile Profile { get; set; } = null!;
        public ICollection<Category> Categories { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
    }

}

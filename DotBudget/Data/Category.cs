using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotBudget.Data
{
    public class Category
    {
        public Category()
        {
            Receipts = new HashSet<Receipt>();
        }
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal BudgetedAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentSpent { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<Receipt> Receipts { get; set; } = null!;


    }
}

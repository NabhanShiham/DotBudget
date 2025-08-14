using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotBudget.Data
{
    public class Receipt
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? ImagePath { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;


        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}

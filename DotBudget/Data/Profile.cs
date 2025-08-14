using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotBudget.Data
{
    public class Profile
    {
        [Key]
        public required string Id { get; set; }
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MainBudget { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentSpending { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; } = null!;
    }
}

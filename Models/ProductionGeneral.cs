using System.ComponentModel.DataAnnotations;

namespace SolutionPal.RazorPages.Models
{
    public class ProductionGeneral
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Batch { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Quantity { get; set; }
        [Required]
        public string? userIdCreation { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
       
       
        [Required]
        public string Location { get; set; }

        [Required]
        public string Final_Level { get; set; }

        public string? Comments { get; set; }
        public string? userIdModification { get; set; }

        public DateTime? ModificationDate { get; set; }
        public string? ModificationComments { get; set; }

    }
}

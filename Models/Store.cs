using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SolutionPal.RazorPages.Models
{
    public class Store
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
        public string userIdCreation { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string userIdModification { get; set; }
        [Required]
        public DateTime ModificationDate { get; set; }
        [Required]
        public string? Comments { get; set; }
    }
}

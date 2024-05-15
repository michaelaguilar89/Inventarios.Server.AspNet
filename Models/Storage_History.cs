using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace SolutionPal.RazorPages.Models
{
    public class Storage_History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Storage_Batch { get; set; }
        [Required]
        public string Storage_ProductName { get; set; }
        [Required]
        public double Storage_Quantity_Actual { get; set; }
        [Required]
        public double Storage_Quantity_Operation { get; set; }
        [Required]
        public double Storage_Quantity_Final { get; set; }
        [Required]
        public string Storage_userIdCreation { get; set; }
        [Required]
        public DateTime Storage_CreationDate { get; set; }
        [AllowNull]
        public string Storage_userIdModification { get; set; }
        [AllowNull]
        public DateTime Storage_ModificationDate { get; set; }
        [AllowNull]
        public string? Storage_Comments { get; set; }
        [AllowNull]
        public int Production_Id { get; set; }
        
        public ICollection<Store> storages { get; set; }
        public ICollection<ProductionGeneral> productions { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Inventarios.Server.AspNet.Dto_s
{
    public class StoreUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Batch { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string userIdModification { get; set; }
        [Required]
        public double newQuantity { get; set; }
        [Required]
        public string opt { get; set; }
        [Required]

        public string? Comments { get; set; }
    }
}

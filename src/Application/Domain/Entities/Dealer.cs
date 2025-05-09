using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Entities
{
    public class Dealer
    {
        public int DealerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cid { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string StaticOtp { get; set; } = string.Empty;

        [Required]
        public string ThemeConfig { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty; // New column
    }
}
using System.ComponentModel.DataAnnotations;
using FoolProof.Core;
using USASales.Models.Dto.ValidationProperties;

namespace USASales.Models.Dto
{
    public class ProductDto
    {
        [Required]
        public long Id { get; set; }

        [Required, StringLength(64)]
        public string Name { get; set; }

        [Required]
        [CategoryIdValidatorProperty]
        public int CategoryId { get; set; }

        [Required, LessThanOrEqualTo(nameof(GrossPrice))]
        public double WholesalePrice { get; set; }

        [Required, GreaterThanOrEqualTo(nameof(WholesalePrice))]
        public double GrossPrice { get; set; }
    }
}

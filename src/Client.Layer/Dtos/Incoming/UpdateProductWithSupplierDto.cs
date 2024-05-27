using System.ComponentModel.DataAnnotations;

namespace Client.Layer.Dtos.Incoming
{
    public class UpdateProductWithSupplierDto
    {

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(1000, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? Description { get; set; }
        public IFormFile? ImagemUpload { get; set; }
        //public string? Image { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        public decimal Value { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        public Guid SupplierId { get; set; }

        //[ScaffoldColumn(false)] /*[ScaffoldColumn(false)] --> não mapear campo quando usar scaffold*/
        //public string SupplierName { get; set; }
    }
}

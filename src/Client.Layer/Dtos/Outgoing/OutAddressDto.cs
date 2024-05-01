using System.ComponentModel.DataAnnotations;

namespace Client.Layer.Dtos.Outgoing
{
    public class OutAddressDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? Street { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(50, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 1)]
        public string? StreetNumber { get; set; }
        public string? Complement { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(8, ErrorMessage = "The {0} field must have {1} characters. (Inválido!)", MinimumLength = 8)]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? Neighborhood { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? City { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(50, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? State { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Client.Layer.Dtos.Incoming
{
    public class SigninUserDto
    {
        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [EmailAddress(ErrorMessage = "The {0} field is in an invalid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 8)]
        public string Password { get; set; }
    }
}

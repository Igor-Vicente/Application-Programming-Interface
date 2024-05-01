﻿using System.ComponentModel.DataAnnotations;

namespace Client.Layer.Dtos.Outgoing
{
    public class OutProductWithSupplierDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        [StringLength(1000, ErrorMessage = "The {0} field must be between {2} and {1} characters. (Inválido!)", MinimumLength = 2)]
        public string? Description { get; set; }
        public string? ImagemUpload { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "The {0} field must be provided. (Obrigatório!)")]
        public decimal Value { get; set; }
        public bool Active { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDocument { get; set; }
        public string? SupplierId { get; set; }
        //[ScaffoldColumn(false)]
        //public DateTime RegistrationDate { get; set; }
    }

}
using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.DTOs.Customer
{
    public class CreateCustomerDTO
    {
        [Required(ErrorMessage = "Nombre del Cliente es Requerido")]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Apellido del Cliente es Requerido")]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Correo Requerido")]
        [StringLength(100)]
        public string email { get; set; }
    }
}

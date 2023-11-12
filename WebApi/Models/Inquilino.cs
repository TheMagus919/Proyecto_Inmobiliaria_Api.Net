using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models{
    [Table("Inquilinos")]
    public class Inquilino
	{
		[Key]
		[Display(Name = "Código")]
		public int IdInquilino { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
        [Display(Name = "Teléfono")]
		public string Telefono { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }

		public string Garante { get; set; }
		public string TelefonoGarante { get; set; }
	}
}
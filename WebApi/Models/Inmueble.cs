using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Inmuebles")]
	public class Inmueble
	{
		[Display(Name = "Código")]
		[Required]
		[Key]
		public int IdInmueble { get; set; }
		[Display(Name = "Dirección")]
		public string Direccion { get; set; } = "";
		public int CantidadDeAmbientes { get; set; }
		public string? Uso { get; set; }
		public string? Tipo { get; set; }
		public double Precio { get; set; }
		public bool Disponible { get; set; }
		public string? Imagen { get; set; }
		[NotMapped]
        public IFormFile? ImagenFile { get; set; }

		[Display(Name = "Dueño")]
		public int IdPropietario { get; set; }
		[ForeignKey(nameof(IdPropietario))]
		[NotMapped]
		public Propietario? Duenio { get; set; }
	}
}


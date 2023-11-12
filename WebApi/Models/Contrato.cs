using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace WebApi.Models{
    [Table("Contratos")]
public class Contrato{
    [Key]
    [Display(Name = "Código contrato")]
    public int IdContrato { get; set; }
    [Required]
    public DateOnly FechaDesde { get; set; }
    [Required]
    public DateOnly FechaHasta { get; set; }
    [Required]
    public double MontoAlquiler { get; set; }
    [Required]
    public int IdInquilino { get; set; }
    [ForeignKey(nameof(IdInquilino))]
    public Inquilino? Vive { get; set; }
    [Required]
    public int IdInmueble { get; set; }
    [ForeignKey(nameof(IdInmueble))]
    public Inmueble? Lugar { get; set; }

    public override string ToString()
        {
            return $" {Vive} ";
        }
}

}
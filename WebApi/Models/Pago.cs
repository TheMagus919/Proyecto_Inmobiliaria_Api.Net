using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models{
    [Table("Pagos")]
public class Pago{
    [Key]
    [Display(Name = "Código pago")]
    public int IdPago { get; set;}
    [Required]
    public int NumeroDePago { get; set;}
    [Required]   
    public DateOnly FechaDePago { get; set;}
    
    [Required]
    public decimal Importe { get; set;}
    [Required]
    public int IdContrato{ get; set;}
    [ForeignKey(nameof(IdContrato))]
    public Contrato? contrato { get; set;}
}
}
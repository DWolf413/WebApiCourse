using System.ComponentModel.DataAnnotations;
using BibliotecaAPI.Validaciones;

namespace BibliotecaAPI.Entidades;

public class Autor
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")] //Si me envian desde un clinete debe tener, eso es del framework
    [StringLength(150, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
    [PrimeraLetraMayuscula]
    public required string Nombre { get; set; } //Aca es validacion de C#
    public List<Libro> Libros { get; set; } = new List<Libro>();
    
    // [Range(18,20)]
    // public int Edad { get; set; }
    // [CreditCard]
    // public string? TarjetaDeCreditos { get; set; }
    // [Url]
    // public string? URL { get; set; }
}
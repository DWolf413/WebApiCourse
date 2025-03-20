using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades;

public class Libro
{
    public int Id { get; set; }
    
    [Required]
    public required string Titulo { get; set; }
    public int AutorId { get; set; } //Llave foranea
    public Autor? Autor { get; set; } //Propiedad de navegacion
}
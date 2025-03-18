using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades;

public class Autor
{
    public int Id { get; set; }
    
    [Required] //Si me envian desde un clinete debe tener, eso es del framework
    
    public required string Nombre { get; set; } //Aca es validacion de C#
    
    public List<Libro> Libros { get; set; } = new List<Libro>();
}
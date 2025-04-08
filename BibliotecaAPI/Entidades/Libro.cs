using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades;

public class Libro : IValidatableObject
{
    public int Id { get; set; }
    
    [Required]
    public required string Titulo { get; set; }
    public int AutorId { get; set; } //Llave foranea
    public Autor? Autor { get; set; } //Propiedad de navegacion
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(Titulo))
        {
            var primeraLetra = Titulo[0].ToString();

            if (primeraLetra != primeraLetra.ToUpper())
            {
                yield return new ValidationResult("La primera letra debe ser mayucula", 
                    new string[] {nameof(Titulo)}); //yield contruye el Ienumerable
            }
        }
    }
}
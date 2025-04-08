using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Validaciones;

public class PrimeraLetraMayusculaAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var primeraLetra = value.ToString()![0].ToString(); // ! Significa que value no es nulo

        if (primeraLetra != primeraLetra.ToUpper())
        {
            return new ValidationResult("La primera letra deb ser mayucula");
        }
        
        return ValidationResult.Success;
    }
}
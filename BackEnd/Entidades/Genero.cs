using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Validaciones;

namespace BackEnd.Entidades
{
    public class Genero : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 10)]
        //[PrimeraLetraMayus] Validacion personalizada por atributo
        public string Nombre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(Nombre)){
                var primeraLetra = Nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper()){
                    yield return new ValidationResult("La primera letra debe ser mayuscula", new string[] {nameof(Nombre)});
                }
            }
            /* Validacion por Modelo -> se ejecutara cuando las demas validacion del net 5 sean correctas, por diseño */
        }
    }
}
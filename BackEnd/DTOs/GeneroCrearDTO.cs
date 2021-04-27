using BackEnd.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTOs
{
    public class GeneroCrearDTO
    {
        //Creamos otro DTO, porque el usuario no introduce el id
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayus] //Validacion personalizada por atributo
        public string Nombre { get; set; }
    }
}

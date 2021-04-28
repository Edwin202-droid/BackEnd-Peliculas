using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTOs
{
    public class CineCrearDTO
    {
        [Required]
        [StringLength(maximumLength: 75)]
        public string Nombre { get; set; }

        //Ubicacion
        [Range(-90,90)]
        public double Latitud { get; set; }

        [Range(-1200,0)]
        public double Longitud { get; set; }
    }
}

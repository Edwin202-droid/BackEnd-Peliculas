using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Entidades
{
    public class Cine
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:75)]
        public string Nombre { get; set; }

        //QuerysEspaciales
        public Point Ubicacion { get; set; } //ubicacion representa: latitud y longitud
    }
}
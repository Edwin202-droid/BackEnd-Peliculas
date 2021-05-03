using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Validaciones;

namespace BackEnd.Entidades
{
    public class PeliculasCines
    {
        public int PeliculaId { get; set; }
        public int CineId { get; set; }
        public Pelicula Pelicula { get; set; } 
        public Cine Cine { get; set; }
        
    }
}
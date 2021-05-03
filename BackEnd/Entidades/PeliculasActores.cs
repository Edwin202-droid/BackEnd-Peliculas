using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Validaciones;

namespace BackEnd.Entidades
{
    public class PeliculasActores
    {
        //Juntamos las 2 llaves en una : Llave compuesta en ApplicationDBContextx
        public int PeliculaId { get; set; }
        public int ActorId { get; set; }
        public Pelicula Pelicula { get; set; }
        public Actor Actor {get; set;}
        //Personaje de un actor en la pelicula
        [StringLength(maximumLength:100)]
        public string Personaje { get; set; }
        //Relevancia del actor
        public int Orden { get; set; }


    }
}


using System.Collections.Generic;

namespace BackEnd.DTOs

{   //Para mostrar en el fronted la pelicula a editar, con los campos de genero cine y actor
    public class PeliculasPutGetDTO
    {
        public PeliculaDTO Pelicula { get; set; }
        public List<GeneroDTO> GenerosSeleccionados { get; set; }
        public List<GeneroDTO> GenerosNoSeleccionados { get; set; }
        public List<CineDTO> CinesSeleccionados { get; set; }
        public List<CineDTO> CinesNoSeleccionados { get; set; }
        public List<PeliculaActorDTO> Actores { get; set; }
    }
}
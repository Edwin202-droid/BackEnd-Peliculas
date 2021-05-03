using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Entidades;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            //Mapeo: Genero y GeneroDTO
            //Genero se encarga de la BD y GeneroDTO de lo que se muestra al usuario 
            CreateMap<Genero, GeneroDTO>().ReverseMap(); //Doble via
            //De generocreacionDTO le pasa a genero
            CreateMap<GeneroCrearDTO, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCrearDTO, Actor>().ForMember(x => x.Foto, options => options.Ignore()); 

            CreateMap<CineCrearDTO, Cine>().ForMember(x => x.Ubicacion, x => x.MapFrom(dto =>
                    geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));

            CreateMap<Cine, CineDTO>()
                    .ForMember(x => x.Latitud, dto =>dto.MapFrom(campo => campo.Ubicacion.Y))
                    .ForMember(x => x.Longitud, dto =>dto.MapFrom(campo => campo.Ubicacion.X));

            //Mapeo Pelicula
            CreateMap<PeliculaCrearDTO, Pelicula>()
                    .ForMember(x => x.Poster, opciones => opciones.Ignore())
                    .ForMember(x => x.PeliculasGeneros, opciones => opciones.MapFrom(MapearPeliculasGeneros))
                    .ForMember(x => x.PeliculasCines, opciones => opciones.MapFrom(MapearPeliculasCines))
                    .ForMember(x => x.PeliculasActores, opciones => opciones.MapFrom(MapearPeliculasActores));
            
            CreateMap<Pelicula,PeliculaDTO>()
                    .ForMember(x=> x.Generos, options => options.MapFrom(MapearPeliculasGeneros))
                    .ForMember(x=>x.Actores, options => options.MapFrom(MapearPeliculasActores))
                    .ForMember(x=> x.Cines, options => options.MapFrom(MapearPeliculasCines));

        }

        private List<GeneroDTO> MapearPeliculasGeneros(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<GeneroDTO>();

            if(pelicula.PeliculasGeneros != null)
            {
                foreach (var genero in pelicula.PeliculasGeneros)
                {
                    resultado.Add(new GeneroDTO() {Id= genero.GeneroId, Nombre= genero.Genero.Nombre});
                }
            }

            return resultado;
        }

        private List<PeliculaActorDTO> MapearPeliculasActores(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<PeliculaActorDTO>();

            if(pelicula.PeliculasActores != null)
            {
                foreach (var actor in pelicula.PeliculasActores)
                {
                    resultado.Add(new PeliculaActorDTO() {  Id= actor.ActorId, 
                                                            Nombre= actor.Actor.Nombre, 
                                                            Foto= actor.Actor.Foto, 
                                                            Orden= actor.Orden,
                                                            Personaje= actor.Personaje});
                }
            }

            return resultado;
        }

        private List<CineDTO> MapearPeliculasCines(Pelicula pelicula, PeliculaDTO peliculaDTO)
        {
            var resultado = new List<CineDTO>();

            if(pelicula.PeliculasCines != null)
            {
                foreach (var cine in pelicula.PeliculasCines)
                {
                    resultado.Add(new CineDTO() {   Id= cine.CineId,
                                                    Nombre= cine.Cine.Nombre,
                                                    Latitud= cine.Cine.Ubicacion.Y,
                                                    Longitud = cine.Cine.Ubicacion.X});
                }
            }

            return resultado;
        }

        private List<PeliculasGeneros> MapearPeliculasGeneros (PeliculaCrearDTO peliculaCreacionDTO , Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();
            if(peliculaCreacionDTO.GenerosIds == null){return resultado;}

            foreach (var id in peliculaCreacionDTO.GenerosIds)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }
            return resultado;
        }

        private List<PeliculasCines> MapearPeliculasCines (PeliculaCrearDTO peliculaCreacionDTO , Pelicula pelicula)
        {
            var resultado = new List<PeliculasCines>();
            if(peliculaCreacionDTO.CinesIds == null){return resultado;}

            foreach (var id in peliculaCreacionDTO.CinesIds)
            {
                resultado.Add(new PeliculasCines() { CineId = id });
            }
            return resultado;
        }

        private List<PeliculasActores> MapearPeliculasActores (PeliculaCrearDTO peliculaCreacionDTO , Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();
            if(peliculaCreacionDTO.Actores == null){return resultado;}

            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.Id, Personaje= actor.Personaje});
            }
            return resultado;
        }


    }
}

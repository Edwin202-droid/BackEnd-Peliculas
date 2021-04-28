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
            CreateMap<ActorCrearDTO, Actor>().ForMember(x => x.Foto, options => options.Ignore()); /* ignoramos la foto */

            CreateMap<CineCrearDTO, Cine>().ForMember(x => x.Ubicacion, x => x.MapFrom(dto =>
                    geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));

            CreateMap<Cine, CineDTO>()
                    .ForMember(x => x.Latitud, dto =>dto.MapFrom(campo => campo.Ubicacion.Y))
                    .ForMember(x => x.Longitud, dto =>dto.MapFrom(campo => campo.Ubicacion.X));
        }
    }
}

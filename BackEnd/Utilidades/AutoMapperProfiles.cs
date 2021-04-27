using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Mapeo: Genero y GeneroDTO
            //Genero se encarga de la BD y GeneroDTO de lo que se muestra al usuario 
            CreateMap<Genero, GeneroDTO>().ReverseMap(); //Doble via
            //De generocreacionDTO le pasa a genero
            CreateMap<GeneroCrearDTO, Genero>();
        }
    }
}

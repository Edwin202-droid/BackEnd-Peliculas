using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Entidades;

namespace BackEnd.Repositorio{
    public interface IRepositorio{
        List<Genero> ObtenerTodosLosGeneros();

        /* Obtener por Id del Repositorio */
        Task<Genero> ObtenerPorId(int Id);

        /* Guid */
        Guid ObtenerGuid();
    }
}
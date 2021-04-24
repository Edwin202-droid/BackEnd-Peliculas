using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Entidades;
using Microsoft.Extensions.Logging;
using BackEnd.Repositorio;

namespace BackEnd.Repositorio{
    public class RepositorioEnMemoria: IRepositorio
    {

        private List<Genero> _generos;
        public RepositorioEnMemoria(ILogger<RepositorioEnMemoria> logger)
        {
            _generos = new List<Genero>(){
                new Genero(){ Id= 1 , Nombre= "Comedia"},
                new Genero(){ Id= 2 , Nombre= "Drama"}
            };

            _guid= Guid.NewGuid(); // 123124_SADASDASD-DFDSFSF 
        }

        public Guid _guid;
        /* services.addsingleton : hace que el valor siempre permanezca, Guild deberia cambiar por cada send en postman
        pero singleto hace que siempre permanezca */

        public List<Genero> ObtenerTodosLosGeneros(){
            return _generos;
        }
        /* Obtener por Id */
        public async Task<Genero> ObtenerPorId(int Id){

            await Task.Delay(1);
            return _generos.FirstOrDefault(x => x.Id == Id);
        }

        public Guid ObtenerGuid(){
            return _guid;
        }
    }
}
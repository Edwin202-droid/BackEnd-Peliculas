using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Entidades;
using BackEnd.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace BackEnd.Controllers{

    /* Ruta del Api */
    [Route("api/generos")]
    [ApiController] //El modelo de una accion es invalido, muestra al usuario el error
    //si no esta autorizados. Error 401
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenerosController: ControllerBase
    {
        private readonly IRepositorio repositorio;
        private readonly ILogger<GenerosController> logger;


        /* En el constructor inyectamos lo que usaremos, y siempre debemos inyectar el private readonly*/
        public GenerosController(IRepositorio repositorio,
                                ILogger<GenerosController> logger)
        {
            this.repositorio = repositorio;
            this.logger = logger;
            this.logger= logger;
        }

        /* Acciones */
        
        /* Tres formas de llamar el listado: reglas de ruteo */
        [HttpGet]                       // api/generos
        [HttpGet ("listado")]           // api/generos/listado
        [HttpGet("/listadoGeneros")]    // /listadoGeneros
        
        public ActionResult<List<Genero>> Get(){

            logger.LogInformation("Vamos a mostrar los generos");
            return repositorio.ObtenerTodosLosGeneros();

        }

        [HttpGet ("guid")] //api/generos/guid
        public ActionResult<Guid> GetGUID(){
            return repositorio.ObtenerGuid();
        }



        /* Obtener un genero por su Id */
        [HttpGet("{Id:int}")] //  api/generos/1
        public async Task<ActionResult<Genero>> Get(int Id, [FromHeader] string nombre){

            logger.LogDebug("Obteniendo un genero por el id {Id}");
            
            var genero = await repositorio.ObtenerPorId(Id);

            if(genero == null){
                logger.LogWarning($"No pudimos encontrar el genero de id {Id}");
                return NotFound();//error 400
            }

            return genero;
        }
        /* ActionResult para hacer funcionar el NotFound, id:int -> solo numero enteros, genero null, si se introduce
        un id invalido entonces genero= null return NOTFOUND, de igual forma si no existe un id */

        /* Task, async, await -> programacion asincrona, espera la respuesta de nuestra base de datos */


        /* FromBody para hacer el posteo */
        [HttpPost]
        public ActionResult Post([FromBody] Genero genero){
            return NoContent();
        }

        [HttpPut]
        public void Put(){
            
        }

        [HttpDelete]
        public void Delete(){
            
        }
    }
}
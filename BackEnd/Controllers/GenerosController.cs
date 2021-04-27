using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Entidades;
using BackEnd.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackEnd.Controllers{

    /* Ruta del Api */
    [Route("api/generos")]
    [ApiController] //El modelo de una accion es invalido, muestra al usuario el error
    //si no esta autorizados. Error 401
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenerosController: ControllerBase
    {
        private readonly ILogger<GenerosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;


        /* En el constructor inyectamos lo que usaremos, y siempre debemos inyectar el private readonly*/
        public GenerosController( ILogger<GenerosController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        /* Acciones */
        [HttpGet] // api/generos       
        public async Task<ActionResult<List<GeneroDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO){

            //var generos = await context.Genero.ToListAsync();
            //Paginacion
            var queryable =  context.Genero.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var generos = await queryable.OrderBy( x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            //todo lo que hay en genero, lo pasa (mapea) a listado de generoDTO que es lo qe se le muestra al usuario
            return mapper.Map<List<GeneroDTO>>(generos);

        }

        /* Obtener un genero por su Id */
        [HttpGet("{Id:int}")] //  api/generos/1
        public async Task<ActionResult<GeneroDTO>> Get(int Id){

            var genero = await context.Genero.FirstOrDefaultAsync(x => x.Id == Id);

            if(genero == null){ return NotFound();}

            return mapper.Map<GeneroDTO>(genero);

        }
        
        /* FromBody para hacer el posteo */
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCrearDTO generoCreacionDTO){

            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id,  [FromBody] GeneroCrearDTO generoCreacionDTO){
             var genero = await context.Genero.FirstOrDefaultAsync(x => x.Id == Id);

            if(genero == null){ return NotFound();}

            genero = mapper.Map(generoCreacionDTO, genero);

            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id){
            var existe = await context.Genero.AnyAsync(x => x.Id == id);

            if(!existe) { return NotFound();}

            context.Remove(new Genero() {Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
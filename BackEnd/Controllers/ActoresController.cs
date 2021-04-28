using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.DTOs;
using BackEnd.Entidades;
using BackEnd.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/actores")]
    [ApiController]
    public class ActoresController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var actores = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            //todo lo que hay en genero, lo pasa (mapea) a listado de generoDTO que es lo qe se le muestra al usuario
            return mapper.Map<List<ActorDTO>>(actores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if(actor == null)
            {
                return NotFound();
            }

            return mapper.Map<ActorDTO>(actor);
        }


        [HttpPost]
        //Cambiamos de FromBody a FromForm para enviar foto
        public async Task<ActionResult> Post([FromForm] ActorCrearDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);

            if(actorCreacionDTO.Foto != null)
            {
                actor.Foto = await almacenadorArchivos.GuardarArchivo(contenedor, actorCreacionDTO.Foto);
            }
            context.Add(actor);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCrearDTO actorCreacionDTO)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if(actor == null)
            {
                return NotFound();
            }

            actor =mapper.Map(actorCreacionDTO, actor);

            if(actorCreacionDTO.Foto != null)
            {
                actor.Foto = await almacenadorArchivos.EditarArchivo(contenedor, actorCreacionDTO.Foto, actor.Foto);
            }

            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id){

            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);

            if(actor == null) { return NotFound();}

            context.Remove(actor);
            await context.SaveChangesAsync();
            //Borrar la foto
            await almacenadorArchivos.BorrarArchivo(actor.Foto, contenedor);
            return NoContent();
        }

    }
}
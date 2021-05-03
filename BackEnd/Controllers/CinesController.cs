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
    [Route("api/cines")]
    [ApiController]
    public class CinesController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet] // api/generos       
        public async Task<ActionResult<List<CineDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO){

            var queryable =  context.Cines.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var cines = await queryable.OrderBy( x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            
            return mapper.Map<List<CineDTO>>(cines);

        }                                                                                      

        [HttpGet("{Id:int}")] //  api/generos/1
        public async Task<ActionResult<CineDTO>> Get(int Id){

            var cine = await context.Cines                 .FirstOrDefaultAsync(x => x.Id == Id);

            if(cine == null){ return NotFound();}

            return mapper.Map<CineDTO>(cine);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CineCrearDTO cineCreacionDTO)
        {
            var cine = mapper.Map<Cine>(cineCreacionDTO);
            context.Add(cine);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id,  [FromBody] CineCrearDTO cineCreacionDTO){
             var cine = await context.Cines.FirstOrDefaultAsync(x => x.Id == Id);

            if(cine == null){ return NotFound();}

            cine = mapper.Map(cineCreacionDTO, cine);

            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id){
            var existe = await context.Cines.AnyAsync(x => x.Id == id);

            if(!existe) { return NotFound();}

            context.Remove(new Cine() {Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}

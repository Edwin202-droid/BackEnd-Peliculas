using BackEnd.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd
{   //Para poder acceder a las tablas de la base de datos
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        //Llave compuesta 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Para la entidad peliculasactores, la llave estara compuesta por actorid y peliculaid
            modelBuilder.Entity<PeliculasActores>().HasKey(x => new { x.ActorId, x.PeliculaId });

            modelBuilder.Entity<PeliculasGeneros>().HasKey(x => new { x.GeneroId, x.PeliculaId });

            modelBuilder.Entity<PeliculasCines>().HasKey(x => new { x.CineId, x.PeliculaId });

            base.OnModelCreating(modelBuilder);
        }

        //Creando la tabla Generos
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<PeliculasCines> PeliculasCines { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        
    }
}

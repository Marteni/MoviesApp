using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.DAL.Entities;

namespace MoviesApp.DAL
{
    public  class MoviesAppDbContext: DbContext
    {
  
        public MoviesAppDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<PersonEntity> People { get; set; }
        public DbSet<MoviesPersonActorEntity> Actors { get; set; }
        public DbSet<MoviesPersonDirectorEntity> Directors { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MovieEntity>().HasMany(a => a.Actors).WithOne(m => m.ActedInMovie)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<MovieEntity>().HasMany(d => d.Directors).WithOne(dm => dm.DirectedMovie)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<PersonEntity>().HasMany(aim => aim.ActedInMovies).WithOne(a => a.Actor)
        //        .OnDelete(DeleteBehavior.Cascade);
        //    modelBuilder.Entity<PersonEntity>().HasMany(dm => dm.DirectedMovies).WithOne(d => d.Director)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.SeedGeorge();
        //    modelBuilder.SeedMark();
        //    modelBuilder.SeedCarry();
        //    modelBuilder.SeedMovie();
        //    modelBuilder.SeedRating();
        //    modelBuilder.SeedActors();
        //    modelBuilder.SeedDirectors();
        //}

    }
}
 
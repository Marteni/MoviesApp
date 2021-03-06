﻿using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL.Entities;

namespace MoviesApp.DAL
{
    public class MoviesAppDbContext: DbContext
    {
  
        public MoviesAppDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<PersonEntity> People { get; set; }
        public DbSet<MoviesPersonActorEntity> Actors { get; set; }
        public DbSet<MoviesPersonDirectorEntity> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedGeorge();
            modelBuilder.SeedMark();
            modelBuilder.SeedCarry();
            modelBuilder.SeedMovie();
            modelBuilder.SeedRating();
        }
    }
}
 
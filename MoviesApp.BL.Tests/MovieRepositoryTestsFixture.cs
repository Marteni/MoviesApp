using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.DAL.Tests;
using MoviesApp.DAL.Factories;
using MoviesApp.BL.Repositories;

namespace MoviesApp.BL.Tests
{
    public class MovieRepositoryTestsFixture : MoviesAppDbContextSetupFixture
    {
        public MovieRepositoryTestsFixture() : base(nameof(MovieRepositoryTestsFixture))
        {
            Repository = new MovieRepository(DbContextFactory);
        }
        public MovieRepository Repository { get; }
    }
}
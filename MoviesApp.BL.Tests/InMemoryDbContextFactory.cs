﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoviesApp.BL.Factories;
using MoviesApp.DAL;

namespace MoviesApp.BL.Tests
{
    public class InMemoryDbContextFactory : IDbContextSqlFactory
    {
        public MoviesAppDbContext CreateAppDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseInMemoryDatabase("MoviesAppDB");
            return new MoviesAppDbContext(optionsBuilder.Options);
        }

    }
}
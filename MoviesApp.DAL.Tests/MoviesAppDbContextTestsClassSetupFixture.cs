using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.DAL.Tests
{
    public class MoviesAppDbContextTestsClassSetupFixture : MoviesAppDbContextSetupFixture
    {
        public MoviesAppDbContext MoviesDbContextSUT { get; }
        public MoviesAppDbContextTestsClassSetupFixture() : base(nameof(MoviesAppDbContextTestsClassSetupFixture)) => MoviesDbContextSUT = DbContextFactory.CreateDbContext();
    }
}

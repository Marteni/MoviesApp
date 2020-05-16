using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MoviesApp.DAL.Factories
{
    public class DbContextSqlFactory : IDbContextSqlFactory
    {
        public MoviesAppDbContext CreateAppDbContext()
        {

            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = MoviesAppDb;MultipleActiveResultSets = True;Integrated Security = True;");
            return new MoviesAppDbContext(optionsBuilder.Options);
        }

    }
}

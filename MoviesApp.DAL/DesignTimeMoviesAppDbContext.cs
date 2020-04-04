using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace MoviesApp.DAL
{
    public class DesignTimeMoviesAppDbContext: IDesignTimeDbContextFactory<MoviesAppDbContext>
    {
      
        public MoviesAppDbContext CreateDbContext(string[] args)
        {

            // var configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();
            // var connectionString = configuration.GetValue<string>("connectionString");
            //var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = TasksDB;MultipleActiveResultSets = True;Integrated Security = True; ";

            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = MoviesAppDb;MultipleActiveResultSets = True;Integrated Security = True; ");
            return new MoviesAppDbContext(optionsBuilder.Options);
        }
    }
}

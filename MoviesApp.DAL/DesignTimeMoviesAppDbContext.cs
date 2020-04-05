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

            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            var connectionString = configuration.GetValue<string>("connectionString");


            var optionsBuilder = new DbContextOptionsBuilder<MoviesAppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MoviesAppDbContext(optionsBuilder.Options);
        }
    }
}

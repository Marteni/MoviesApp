using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.DAL;

namespace MoviesApp.BL.Factories
{
    public interface IDbContextSqlFactory
    {
        MoviesAppDbContext CreateAppDbContext();
    }
}

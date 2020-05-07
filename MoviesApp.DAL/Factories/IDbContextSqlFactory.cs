namespace MoviesApp.DAL.Factories
{
    public interface IDbContextSqlFactory
    {
        MoviesAppDbContext CreateAppDbContext();

    }
}

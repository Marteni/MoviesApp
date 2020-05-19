namespace MoviesApp.DAL.Factories
{
    public interface IDbContextFactory
    {
        MoviesAppDbContext CreateDbContext();

    }
}

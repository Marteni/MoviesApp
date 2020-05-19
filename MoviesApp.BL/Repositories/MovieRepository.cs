using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly IDbContextFactory _dbContextSqlFactory;

        public MovieRepository(IDbContextFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<MovieListModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Movies
                    .Select(e => MovieMapper.MapMovieEntityToListModel(e))
                    .ToList();
            }
        }

        public IList<MovieDetailModel> GetAllDetails()
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Movies
                    .Select(e => MovieMapper.MapMovieEntityToDetailModel(e))
                    .ToList();
            }
        }

        public MovieDetailModel GetById(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = dbContext.Movies.First(t => t.Id == id);
                return MovieMapper.MapMovieEntityToDetailModel(entity);
            }
        }

        public MovieListModel GetByIdListModel(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = dbContext.Movies.FirstOrDefault(t => t.Id == id);
                if (entity == null) return null;
                return MovieMapper.MapMovieEntityToListModel(entity);
            }
        }

        public MovieDetailModel Create(MovieDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = MovieMapper.MapMovieDetailModelToEntity(model);
                dbContext.Movies.Add(entity);
                dbContext.SaveChanges();
                return MovieMapper.MapMovieEntityToDetailModel(entity);
            }
        }

        public void Update(MovieDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = MovieMapper.MapMovieDetailModelToEntity(model);
                dbContext.Movies.Update(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = dbContext.Movies.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.BL.Factories;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;

namespace MoviesApp.BL.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly IDbContextSqlFactory _dbContextSqlFactory;

        public MovieRepository(IDbContextSqlFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<MovieListModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.Movies
                    .Select(e => MovieMapper.MapMovieEntityToListModel(e))
                    .ToList();
            }
        }

        public MovieDetailModel GetById(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Movies.First(t => t.Id == id);
                return MovieMapper.MapMovieEntityToDetailModel(entity);
            }
        }

        public MovieDetailModel Create(MovieDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = MovieMapper.MapMovieDetailModelToEntity(model);
                dbContext.Movies.Add(entity);
                dbContext.SaveChanges();
                return MovieMapper.MapMovieEntityToDetailModel(entity);
            }
        }

        public void Update(MovieDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = MovieMapper.MapMovieDetailModelToEntity(model);
                dbContext.Movies.Update(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Movies.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}

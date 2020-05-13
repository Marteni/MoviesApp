using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Repositories
{
    public class MoviePersonDirectorRepository : IMoviePersonDirectorRepository
    {
        private readonly IDbContextSqlFactory _dbContextSqlFactory;
        public MoviePersonDirectorRepository(IDbContextSqlFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<PersonDirectorDetailModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.Directors
                    .Select(e => PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public IList<PersonDirectorDetailModel> GetAllMovieDirectorByMovieId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.Directors
                    .Where(t => t.MovieId == id)
                    .Select(e => PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(e))
                    .ToList();

            }
        }

        public IList<PersonDirectorDetailModel> GetAllMovieDirectorByDirectorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.Directors
                    .Where(t => t.DirectorId == id)
                    .Select(e => PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(e))
                    .ToList();

            }
        }

        public PersonDirectorDetailModel Create(PersonDirectorDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = PersonDirectorMapper.MapPersonDirectorDetailModelToEntity(model);
                dbContext.Directors.Add(entity);
                dbContext.SaveChanges();
                return PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(entity);
            }
        }

        public void Update(PersonDirectorDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = PersonDirectorMapper.MapPersonDirectorDetailModelToEntity(model);
                dbContext.Directors.Update(entity);
                dbContext.SaveChanges();
            }
        }

        public void TryDeleteByDirectorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Directors.First(t => t.DirectorId == id);
                if (entity != null)
                {
                    dbContext.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public void TryDeleteByMovieId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Directors.First(t => t.MovieId == id);
                if (entity != null)
                {
                    dbContext.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Repositories
{
    public class MoviePersonActorRepository : IMoviePersonActorRepository
    {
        private readonly IDbContextSqlFactory _dbContextSqlFactory;
        public MoviePersonActorRepository(IDbContextSqlFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<PersonActorDetailModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.Actors
                    .Select(e => PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public IList<PersonActorDetailModel> GetAllMovieActorByMovieId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                //SELECT * FROM Ingredient WHERE Movie.Id = id;
                return dbContext.Actors
                    .Where(t => t.MovieId == id)
                    .Select(e => PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(e))
                    .ToList();

            }
        }

        public IList<PersonActorDetailModel> GetAllMovieActorByActorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                //SELECT * FROM Ingredient WHERE Movie.Id = id;
                return dbContext.Actors
                    .Where(t => t.ActorId == id)
                    .Select(e => PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(e))
                    .ToList();

            }
        }

        public PersonActorDetailModel Create(PersonActorDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = PersonActorMapper.MapPersonActorDetailModelToEntity(model);
                dbContext.Actors.Add(entity);
                dbContext.SaveChanges();
                return PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(entity);
            }
        }

        public void Update(PersonActorDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = PersonActorMapper.MapPersonActorDetailModelToEntity(model);
                dbContext.Actors.Update(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Actors.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        
    }
}

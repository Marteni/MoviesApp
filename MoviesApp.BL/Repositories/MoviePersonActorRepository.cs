using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Repositories
{
    public class MoviePersonActorRepository : IMoviePersonActorRepository
    {
        private readonly IDbContextFactory _dbContextSqlFactory;
        public MoviePersonActorRepository(IDbContextFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<PersonActorDetailModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Actors
                    .Select(e => PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public IList<PersonActorDetailModel> GetAllMovieActorByMovieId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Actors
                    .Where(t => t.MovieId == id)
                    .Select(e => PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(e))
                    .ToList();

            }
        }

        public IList<PersonActorDetailModel> GetAllMovieActorByActorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Actors
                    .Where(t => t.ActorId == id)
                    .Select(e => PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public PersonActorDetailModel Create(PersonActorDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = PersonActorMapper.MapPersonActorDetailModelToEntity(model);
                dbContext.Actors.Add(entity);
                dbContext.SaveChanges();
                return PersonActorMapper.MapMoviesPersonActorEntityToDetailModel(entity);
            }
        }

        public void TryDeleteActorMovieRelation(Guid movieId, Guid actorId)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = dbContext.Actors.FirstOrDefault(t => t.MovieId == movieId && t.ActorId == actorId);

                if (entity != null)
                {
                    dbContext.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public void TryDeleteAllByMovieOrActorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var allActorMovieRelationships = dbContext.Actors
                    .Where(t => t.ActorId == id || t.MovieId == id)
                    .ToList();

                foreach (var actorMovieRelationship in allActorMovieRelationships)
                {
                    if (actorMovieRelationship != null)
                    {
                        dbContext.Remove(actorMovieRelationship);
                    }
                }
                dbContext.SaveChanges();
            }
        }

    }
}

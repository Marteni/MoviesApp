﻿using System;
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

        public void TryDeleteByActorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Actors.FirstOrDefault(t => t.ActorId == id);
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
              
                    var entity = dbContext.Actors.FirstOrDefault(t => t.MovieId == id);
                    if (entity != null)
                    {
                        dbContext.Remove(entity);
                        dbContext.SaveChanges();
                    }
              
                    
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Repositories
{
    public class MoviePersonDirectorRepository : IMoviePersonDirectorRepository
    {
        private readonly IDbContextFactory _dbContextSqlFactory;
        public MoviePersonDirectorRepository(IDbContextFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<PersonDirectorDetailModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Directors
                    .Select(e => PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public IList<PersonDirectorDetailModel> GetAllMovieDirectorByMovieId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Directors
                    .Where(t => t.MovieId == id)
                    .Select(e => PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public IList<PersonDirectorDetailModel> GetAllMovieDirectorByDirectorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                return dbContext.Directors
                    .Where(t => t.DirectorId == id)
                    .Select(e => PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(e))
                    .ToList();
            }
        }

        public PersonDirectorDetailModel Create(PersonDirectorDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var entity = PersonDirectorMapper.MapPersonDirectorDetailModelToEntity(model);
                dbContext.Directors.Add(entity);
                dbContext.SaveChanges();
                return PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel(entity);
            }
        }

        public void TryDeleteDirectorMovieRelation(Guid movieId, Guid directorId)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var directorMovieRelation = dbContext.Directors.FirstOrDefault(t => t.MovieId == movieId && t.DirectorId == directorId);

                if (directorMovieRelation != null)
                {
                    dbContext.Remove(directorMovieRelation);
                    dbContext.SaveChanges();
                }
            }
        }

        public void TryDeleteAllByMovieOrDirectorId(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateDbContext())
            {
                var allDirectorMovieRelationships = dbContext.Directors
                    .Where(t => t.DirectorId == id || t.MovieId == id)
                    .ToList();

                foreach (var directorMovieRelationship in allDirectorMovieRelationships)
                {
                    if (directorMovieRelationship != null)
                    {
                        dbContext.Remove(directorMovieRelationship);
                    }
                }

                dbContext.SaveChanges();
            }
        }

    }
}

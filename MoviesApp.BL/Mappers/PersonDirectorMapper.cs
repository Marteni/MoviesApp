using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    internal static class PersonDirectorMapper
    {
        public static PersonDirectorDetailModel MapMoviesPersonDirectorEntityToDetailModel(MoviesPersonDirectorEntity entity)
        {
            return new PersonDirectorDetailModel
            {
                DirectorId = entity.DirectorId,
                MovieId = entity.MovieId,
                Name = entity.Director.Name,
                Surname = entity.Director.Surname,
                Age = entity.Director.Age,
                PictureUrl = entity.Director.PictureUrl,
            };
        }

        public static MoviesPersonDirectorEntity MapPersonDirectorDetailModelToEntity(PersonDirectorDetailModel model)
        {
            return new MoviesPersonDirectorEntity
            {
                Id = model.Id,
                DirectorId = model.DirectorId,
                MovieId = model.MovieId
            };
        }
    }
}
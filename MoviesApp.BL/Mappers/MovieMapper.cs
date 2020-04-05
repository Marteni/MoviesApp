using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesApp.BL.Mappers
{
    internal static class MovieMapper
    {
        public static MovieListModel MapMovieEntityToListModel(MovieEntity entity)
        {
            return new MovieListModel
            {
                Id = entity.Id,
                Name = entity.OriginalTitle
            };
        }

        public static MovieDetailModel MapMovieEntityToDetailModel(MovieEntity entity)
        {
            return new MovieDetailModel
            {
                Id = entity.Id,
                OriginalTitle = entity.OriginalTitle,
                CzechTitle = entity.CzechTitle,
                Genre = (GenreType) entity.Genre,
                PosterImageUrl = entity.PosterImageUrl,
                CountryOfOrigin = entity.CountryOfOrigin,
                Length = entity.Length,
                Description = entity.Description,
                Actors = entity.Actors.Select(PersonActorMapper.MapMoviesPersonActorEntityToDetailModel).ToList(),
                Directors = entity.Directors.Select(PersonDirectorMapper.MapMoviesPersonDirectorEntityToDetailModel).ToList()
            };
        }

        public static MovieEntity MapMovieDetailModelToEntity(MovieDetailModel model)
        {
            return new MovieEntity
            {
                Id = model.Id,
                OriginalTitle = model.OriginalTitle,
                CzechTitle = model.CzechTitle,
                Genre = (GenreType) model.Genre,
                PosterImageUrl = model.PosterImageUrl,
                CountryOfOrigin = model.CountryOfOrigin,
                Length = model.Length,
                Description = model.Description,
                Actors = model.Actors.Select(PersonActorMapper.MapPersonActorDetailModelToEntity).ToList(),
                Directors = model.Directors.Select(PersonDirectorMapper.MapPersonDirectorDetailModelToEntity).ToList()
            };
        }
    }
}

using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Mappers
{
    internal static class MovieMapper
    {
        public static MovieListModel MapIngredientEntityToListModel(MovieEntity entity)
        {
            return new MovieListModel
            {
                Id = entity.Id,
                Name = entity.OriginalTitle
                

            };
        }

        public static MovieDetailModel MapIngredientEntityToDetailModel(MovieEntity entity)
        {
            return new MovieDetailModel
            {
                Id = entity.Id,
                OriginalTitle = entity.OriginalTitle,
                CzechTitle = entity.CzechTitle,
                Genre = entity.Genre,
                PosterImageUrl = entity.PosterImageUrl,
                CountryOfOrigin = entity.CountryOfOrigin,
                Length = entity.Length,
                Description = entity.Description
                
            };
        }

        public static IngredientEntity MapIngredientDetailModelToEntity(IngredientDetailModel model)
        {
            return new IngredientEntity
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name
            };
        }
    }
}

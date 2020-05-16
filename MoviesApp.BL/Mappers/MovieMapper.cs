using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    public static class MovieMapper
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
                Genre = entity.Genre,
                PosterImageUrl = entity.PosterImageUrl,
                CountryOfOrigin = entity.CountryOfOrigin,
                Length = entity.Length,
                Description = entity.Description
            };
        }

        public static MovieEntity MapMovieDetailModelToEntity(MovieDetailModel model)
        {
            return new MovieEntity
            {
                Id = model.Id,
                OriginalTitle = model.OriginalTitle,
                CzechTitle = model.CzechTitle,
                Genre = model.Genre,
                PosterImageUrl = model.PosterImageUrl,
                CountryOfOrigin = model.CountryOfOrigin,
                Length = model.Length,
                Description = model.Description
            };
        }
    }
}

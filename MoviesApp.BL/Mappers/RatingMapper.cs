using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    internal static class RatingMapper
    {
        public static RatingListModel MapRatingEntityToListModel(RatingEntity entity)
        {
            return new RatingListModel
            {
                Id = entity.Id,
                Nick = entity.Nick            };
        }

        public static RatingDetailModel MapRatingEntityToDetailModel(RatingEntity enity)
        {
            return new RatingDetailModel
            {
                Id = enity.Id,
                RatedMovieId = enity.RatedMovieId,
                Nick = enity.Nick,
                NumericEvaluation = enity.NumericEvaluation,
                Review = enity.Review
            };
        }

        public static RatingEntity MapRatingDetailModelToEntity(RatingDetailModel model)
        {
            return new RatingEntity
            {
                Id = model.Id,
                RatedMovieId = model.RatedMovieId,
                Nick = model.Nick,
                NumericEvaluation = model.NumericEvaluation,
                Review = model.Review
            };
        }
    }
}
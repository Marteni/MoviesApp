using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    public static class RatingMapper
    {
        public static RatingDetailModel MapRatingEntityToDetailModel(RatingEntity entity)
        {
            return new RatingDetailModel
            {
                Id = entity.Id,
                RatedMovieId = entity.RatedMovieId,
                Nick = entity.Nick,
                NumericEvaluation = entity.NumericEvaluation,
                Review = entity.Review
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
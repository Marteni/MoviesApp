using System;
using System.Collections.Generic;
using MoviesApp.BL.Models;

namespace MoviesApp.BL.Repositories
{
    public interface IRatingRepository
    {
        IList<RatingDetailModel> GetAll();
        IList<RatingDetailModel> GetAllByMovieId(Guid id);
        RatingDetailModel Create(RatingDetailModel model);
        void Update(RatingDetailModel model);
        void Delete(Guid id);
    }
}

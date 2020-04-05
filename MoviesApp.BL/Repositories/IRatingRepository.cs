using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.BL.Models;

namespace MoviesApp.BL.Repositories
{
    public interface IRatingRepository
    {
        IList<RatingListModel> GetAll();
        RatingDetailModel GetById(Guid id);
        RatingDetailModel Create(RatingDetailModel model);
        void Update(RatingDetailModel model);
        void Delete(Guid id);
    }
}

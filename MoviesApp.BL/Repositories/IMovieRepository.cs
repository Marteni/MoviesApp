using MoviesApp.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Repositories
{
    public interface IMovieRepository
    {
        IList<MovieListModel> GetAll();
        MovieDetailModel GetById(Guid id);
        MovieDetailModel Create(MovieDetailModel model);
        void Update(MovieDetailModel model);
        void Delete(Guid id);
    }
}

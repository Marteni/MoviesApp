﻿using MoviesApp.BL.Models;
using System;
using System.Collections.Generic;

namespace MoviesApp.BL.Repositories
{
    public interface IMovieRepository
    {
        IList<MovieListModel> GetAll();
        IList<MovieDetailModel> GetAllDetails();
        MovieDetailModel GetById(Guid id);
        MovieListModel GetByIdListModel(Guid id);
        MovieDetailModel Create(MovieDetailModel model);
        void Update(MovieDetailModel model);
        void Delete(Guid id);
    }
}

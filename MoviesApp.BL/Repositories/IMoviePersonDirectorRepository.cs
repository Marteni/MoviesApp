using System;
using System.Collections.Generic;
using MoviesApp.BL.Models;

namespace MoviesApp.BL.Repositories
{
    public interface IMoviePersonDirectorRepository
    {
        IList<PersonDirectorDetailModel> GetAll();
        IList<PersonDirectorDetailModel> GetAllMovieDirectorByMovieId(Guid id);
        IList<PersonDirectorDetailModel> GetAllMovieDirectorByDirectorId(Guid id);
        PersonDirectorDetailModel Create(PersonDirectorDetailModel model);
        void Update(PersonDirectorDetailModel model);
        void TryDeleteDirectorMovieRelation(Guid movieId, Guid directorId);
        void TryDeleteAllByMovieOrDirectorId(Guid id);
    }
}

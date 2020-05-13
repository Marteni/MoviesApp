using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.BL.Models;

namespace MoviesApp.BL.Repositories
{
    public interface IMoviePersonActorRepository
    {
        IList<PersonActorDetailModel> GetAll();
        IList<PersonActorDetailModel> GetAllMovieActorByMovieId(Guid id);
        IList<PersonActorDetailModel> GetAllMovieActorByActorId(Guid id);
        PersonActorDetailModel Create(PersonActorDetailModel model);
        void Update(PersonActorDetailModel model);
        void TryDeleteByActorId(Guid id);
        void TryDeleteByMovieId(Guid id);
    }
}

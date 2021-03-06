﻿using System;
using System.Collections.Generic;
using MoviesApp.BL.Models;

namespace MoviesApp.BL.Repositories
{
    public interface IMoviePersonActorRepository
    {
        IList<PersonActorDetailModel> GetAll();
        IList<PersonActorDetailModel> GetAllMovieActorByMovieId(Guid id);
        IList<PersonActorDetailModel> GetAllMovieActorByActorId(Guid id);
        PersonActorDetailModel Create(PersonActorDetailModel model);
        void TryDeleteActorMovieRelation(Guid movieId, Guid actorId);
        void TryDeleteAllByMovieOrActorId(Guid id);
    }
}

using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    public static class PersonActorMapper
    {
        public static PersonActorDetailModel MapMoviesPersonActorEntityToDetailModel(MoviesPersonActorEntity entity)
        {
            return new PersonActorDetailModel
            {
                Id = entity.Id,
                ActorId = entity.ActorId,
                MovieId = entity.MovieId
            };
        }

        public static MoviesPersonActorEntity MapPersonActorDetailModelToEntity(PersonActorDetailModel model)
        {
            return new MoviesPersonActorEntity
            {
                Id = model.Id,
                ActorId = model.ActorId,
                MovieId = model.MovieId
            };
        }
    }
}

using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    internal static class PersonActorMapper
    {
        public static PersonActorDetailModel MapMoviesPersonActorEntityToDetailModel(MoviesPersonActorEntity entity)
        {
            return new PersonActorDetailModel
            {
                ActorId = entity.ActorId,
                MovieId = entity.MovieId,
                Name = entity.Actor.Name,
                Surname = entity.Actor.Surname,
                Age = entity.Actor.Age,
                PictureUrl = entity.Actor.PictureUrl
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

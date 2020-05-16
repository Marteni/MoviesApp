using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    public static class PersonDirectorMapper
    {
        public static PersonDirectorDetailModel MapMoviesPersonDirectorEntityToDetailModel(MoviesPersonDirectorEntity entity)
        {
            return new PersonDirectorDetailModel
            {
                Id = entity.Id,
                DirectorId = entity.DirectorId,
                MovieId = entity.MovieId
            };
        }

        public static MoviesPersonDirectorEntity MapPersonDirectorDetailModelToEntity(PersonDirectorDetailModel model)
        {
            return new MoviesPersonDirectorEntity
            {
                Id = model.Id,
                DirectorId = model.DirectorId,
                MovieId = model.MovieId
            };
        }
    }
}
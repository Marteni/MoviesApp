using MoviesApp.BL.Models;
using MoviesApp.DAL.Entities;

namespace MoviesApp.BL.Mappers
{
    public static class PersonMapper
    {
        public static PersonListModel MapPersonEntityToListModel(PersonEntity entity)
        {
            return new PersonListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname
            };
        }

        public static PersonDetailModel MapPersonEntityToDetailModel(PersonEntity entity)
        {
            return new PersonDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Age = entity.Age,
                PictureUrl = entity.PictureUrl
            };
        }

        public static PersonEntity MapPersonDetailModelToEntity(PersonDetailModel model)
        {
            return new PersonEntity
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age,
                PictureUrl = model.PictureUrl
            };
        }
    }
}

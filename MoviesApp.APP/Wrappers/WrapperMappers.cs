using System;
using MoviesApp.BL.Models;

namespace MoviesApp.APP.Wrappers
{
    public static class WrapperMappers
    {
        public static PersonDetailModel ToPersonDetailModel(dynamic personWrapper)
        {
            return new PersonDetailModel
            {
                Id = personWrapper?.Id,
                Name = personWrapper?.Name,
                Surname = personWrapper?.Surname,
                Age = personWrapper?.Age,
                PictureUrl = personWrapper?.PictureUrl
            };
        }

        public static PersonEditWrapper PersonDetailToPersonEditWrapper(PersonDetailModel personDetail)
        {
            return new PersonEditWrapper
            {
                Id = personDetail.Id,
                Name = personDetail.Name,
                Surname = personDetail.Surname,
                Age = personDetail.Age,
                PictureUrl = personDetail.PictureUrl
            };
        }
        public static PersonNewWrapper PersonDetailToPersonNewWrapper(PersonDetailModel personDetail)
        {
            return new PersonNewWrapper
            {
                Id = personDetail.Id,
                Name = personDetail.Name,
                Surname = personDetail.Surname,
                Age = personDetail.Age,
                PictureUrl = personDetail.PictureUrl
            };
        }
        public static PersonNewFilledWrapper PersonDetailToPersonNewFilledWrapper(PersonDetailModel personDetail)
        {
            return new PersonNewFilledWrapper
            {
                Id = personDetail.Id,
                Name = personDetail.Name,
                Surname = personDetail.Surname,
                Age = personDetail.Age,
                PictureUrl = personDetail.PictureUrl
            };
        }
        public static PersonSelectedWrapper PersonDetailToPersonSelectedWrapper(PersonDetailModel personDetail)
        {
            return new PersonSelectedWrapper
            {
                Id = personDetail.Id,
                Name = personDetail.Name,
                Surname = personDetail.Surname,
                Age = personDetail.Age,
                PictureUrl = personDetail.PictureUrl
            };
        }
        
        public static PersonDeleteGuidWrapper GuidToPersonDeleteGuidWrapper(Guid id)
        {
            return new PersonDeleteGuidWrapper
            {
                Id = id
            };
        }
        public static Guid PersonDeleteGuidWrapperToGuid(PersonDeleteGuidWrapper guidWrapper)
        {
            return guidWrapper.Id;
        }


        public static MovieDetailModel ToMovieDetailModel(dynamic movieWrapper)
        {
            return new MovieDetailModel
            {
                Id = movieWrapper?.Id,
                OriginalTitle = movieWrapper?.OriginalTitle,
                CzechTitle = movieWrapper?.CzechTitle,
                Genre = movieWrapper?.Genre,
                CountryOfOrigin = movieWrapper?.CountryOfOrigin,
                Length = movieWrapper?.Length,
                Description = movieWrapper?.Description,
                PosterImageUrl = movieWrapper?.PosterImageUrl
            };
        }

        public static MovieNewWrapper MovieDetailToMovieNewWrapper(MovieDetailModel movieDetailModel)
        {
            return new MovieNewWrapper
            {
                Id = movieDetailModel.Id,
                OriginalTitle = movieDetailModel.OriginalTitle,
                CzechTitle = movieDetailModel.CzechTitle,
                Genre = movieDetailModel.Genre,
                CountryOfOrigin = movieDetailModel.CountryOfOrigin,
                Length = movieDetailModel.Length,
                Description = movieDetailModel.Description,
                PosterImageUrl = movieDetailModel.PosterImageUrl
            };
        }
        public static MovieNewFilledWrapper MovieDetailToMovieNewFilledWrapper(MovieDetailModel movieDetailModel)
        {
            return new MovieNewFilledWrapper
            {
                Id = movieDetailModel.Id,
                OriginalTitle = movieDetailModel.OriginalTitle,
                CzechTitle = movieDetailModel.CzechTitle,
                Genre = movieDetailModel.Genre,
                CountryOfOrigin = movieDetailModel.CountryOfOrigin,
                Length = movieDetailModel.Length,
                Description = movieDetailModel.Description,
                PosterImageUrl = movieDetailModel.PosterImageUrl
            };
        }
        public static MovieEditWrapper MovieDetailToMovieEditWrapper(MovieDetailModel movieDetailModel)
        {
            return new MovieEditWrapper
            {
                Id = movieDetailModel.Id,
                OriginalTitle = movieDetailModel.OriginalTitle,
                CzechTitle = movieDetailModel.CzechTitle,
                Genre = movieDetailModel.Genre,
                CountryOfOrigin = movieDetailModel.CountryOfOrigin,
                Length = movieDetailModel.Length,
                Description = movieDetailModel.Description,
                PosterImageUrl = movieDetailModel.PosterImageUrl
            };
        }
        public static MovieSelectedWrapper MovieDetailToMovieSelectedWrapper(MovieDetailModel movieDetailModel)
        {
            return new MovieSelectedWrapper
            {
                Id = movieDetailModel.Id,
                OriginalTitle = movieDetailModel.OriginalTitle,
                CzechTitle = movieDetailModel.CzechTitle,
                Genre = movieDetailModel.Genre,
                CountryOfOrigin = movieDetailModel.CountryOfOrigin,
                Length = movieDetailModel.Length,
                Description = movieDetailModel.Description,
                PosterImageUrl = movieDetailModel.PosterImageUrl
            };
        }

        public static MovieDeleteGuidWrapper GuidToMovieDeleteGuidWrapper(Guid id)
        {
            return new MovieDeleteGuidWrapper
            {
                Id = id
            };
        }
        public static Guid MovieDeleteGuidWrapperToGuid(MovieDeleteGuidWrapper guidWrapper)
        {
            return guidWrapper.Id;
        }
    }
}

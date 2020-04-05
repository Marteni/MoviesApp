using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.DAL.Enums;

namespace MoviesApp.BL.Models
{
    public class MovieDetailModel: ModelBase
    {
        public string OriginalTitle { get; set; }
        public string CzechTitle { get; set; }
        public GenreType Genre { get; set; }
        public string PosterImageUrl { get; set; }
        public string CountryOfOrigin { get; set; }
        public TimeSpan Length { get; set; }
        public string Description { get; set; }
        public ICollection<RatingListModel> Ratings { get; set; } = new List<RatingListModel>();
        public ICollection<PersonActorDetailModel> Actors { get; set; } = new List<PersonActorDetailModel>();
        public ICollection<PersonDirectorDetailModel> Directors { get; set; } = new List<PersonDirectorDetailModel>();

        private sealed class MovieDetailModelEqualityComparer : IEqualityComparer<MovieDetailModel>
        {
            public bool Equals(MovieDetailModel x, MovieDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.OriginalTitle == y.OriginalTitle
                       && x.CzechTitle == y.CzechTitle
                       && x.Genre == y.Genre
                       && x.PosterImageUrl == y.PosterImageUrl
                       && x.CountryOfOrigin == y.CountryOfOrigin
                       && x.Length.Equals(y.Length)
                       && x.Description == y.Description
                       && x.Actors.OrderBy(actor => actor.Id).SequenceEqual(y.Actors.OrderBy(actor => actor.Id), PersonActorDetailModel.PersonActorDetailModelComparer)
                       && x.Directors.OrderBy(director => director.Id).SequenceEqual(y.Directors.OrderBy(director => director.Id), PersonDirectorDetailModel.PersonDirectorDetailModelComparer)
                       && x.Ratings.OrderBy(rating => rating.Id).SequenceEqual(y.Ratings.OrderBy(rating => rating.Id));
            }

            public int GetHashCode(MovieDetailModel obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.OriginalTitle);
                hashCode.Add(obj.CzechTitle);
                hashCode.Add((int)obj.Genre);
                hashCode.Add(obj.PosterImageUrl);
                hashCode.Add(obj.CountryOfOrigin);
                hashCode.Add(obj.Length);
                hashCode.Add(obj.Description);
                hashCode.Add(obj.Actors);
                hashCode.Add(obj.Directors);
                hashCode.Add(obj.Ratings);
                return hashCode.ToHashCode();
            }
        }

        public static IEqualityComparer<MovieDetailModel> MovieDetailModelComparer { get; } = new MovieDetailModelEqualityComparer();

        private sealed class MovieDetailModelWithoutCollectionsEqualityComparer : IEqualityComparer<MovieDetailModel>
        {
            public bool Equals(MovieDetailModel x, MovieDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.OriginalTitle == y.OriginalTitle
                       && x.CzechTitle == y.CzechTitle
                       && x.Genre == y.Genre
                       && x.PosterImageUrl == y.PosterImageUrl
                       && x.CountryOfOrigin == y.CountryOfOrigin
                       && x.Length.Equals(y.Length)
                       && x.Description == y.Description;
            }

            public int GetHashCode(MovieDetailModel obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.OriginalTitle);
                hashCode.Add(obj.CzechTitle);
                hashCode.Add((int)obj.Genre);
                hashCode.Add(obj.PosterImageUrl);
                hashCode.Add(obj.CountryOfOrigin);
                hashCode.Add(obj.Length);
                hashCode.Add(obj.Description);
                return hashCode.ToHashCode();
            }
        }

        public static IEqualityComparer<MovieDetailModel> MovieDetailModelWithoutCollectionsComparer { get; } = new MovieDetailModelWithoutCollectionsEqualityComparer();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MoviesApp.DAL.Enums;

namespace MoviesApp.DAL.Entities
{
    public class MovieEntity : EntityBase
    {
        public string OriginalTitle { get; set; }
        public string CzechTitle { get; set; }
        public GenreType Genre { get; set; }
        public string PosterImageUrl { get; set; }
        public string CountryOfOrigin { get; set; }
        public TimeSpan Length { get; set; }
        public string Description { get; set; }

        private sealed class MovieEntityEqualityComparer : IEqualityComparer<MovieEntity>
        {
            public bool Equals(MovieEntity x, MovieEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id 
                       && x.OriginalTitle == y.OriginalTitle 
                       && x.CzechTitle == y.CzechTitle 
                       && x.Genre == y.Genre 
                       && x.PosterImageUrl == y.PosterImageUrl 
                       && x.CountryOfOrigin == y.CountryOfOrigin 
                       && x.Length.Equals(y.Length) 
                       && x.Description == y.Description;
            }

            public int GetHashCode(MovieEntity obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.Id);
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

        public static IEqualityComparer<MovieEntity> MovieComparer { get; } = new MovieEntityEqualityComparer();
        public static IEqualityComparer<MovieEntity> MovieWithoutCollectionsComparer { get; } = new MovieEntityEqualityComparer();
    }
}
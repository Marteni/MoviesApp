using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApp.DAL.Entities
{
    public class PersonEntity : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<MoviesPersonActorEntity> ActedInMovies { get; set; } = new List<MoviesPersonActorEntity>();
        public ICollection<MoviesPersonDirectorEntity> DirectedMovies { get; set; } = new List<MoviesPersonDirectorEntity>();
        //public ICollection<RatingEntity> GivenRatings { get; set; } = new List<RatingEntity>();

        private sealed class PersonEntityEqualityComparer : IEqualityComparer<PersonEntity>
        {
            public bool Equals(PersonEntity x, PersonEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id
                       && x.Name == y.Name
                       && x.Surname == y.Surname
                       && x.Age == y.Age
                       && x.PictureUrl == y.PictureUrl
                       && x.ActedInMovies.OrderBy(actor => actor.Id).SequenceEqual(
                           y.ActedInMovies.OrderBy(actor => actor.Id),
                           MoviesPersonActorEntity.MoviesPersonActorEntityComparer)
                       && x.DirectedMovies.OrderBy(director => director.Id).SequenceEqual(
                           y.DirectedMovies.OrderBy(director => director.Id),
                           MoviesPersonDirectorEntity.MoviesPersonDirectorEntityComparer);
            }

            public int GetHashCode(PersonEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.Surname, obj.Age, obj.PictureUrl, obj.ActedInMovies, obj.DirectedMovies);
            }
        }

        public static IEqualityComparer<PersonEntity> PersonComparer { get; } = new PersonEntityEqualityComparer();

        private sealed class PersonWithoutCollectionsEqualityComparer : IEqualityComparer<PersonEntity>
        {
            public bool Equals(PersonEntity x, PersonEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Name == y.Name && x.Surname == y.Surname && x.Age == y.Age && x.PictureUrl == y.PictureUrl;
            }

            public int GetHashCode(PersonEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.Surname, obj.Age, obj.PictureUrl);
            }
        }

        public static IEqualityComparer<PersonEntity> PersonWithoutCollectionsComparer { get; } = new PersonWithoutCollectionsEqualityComparer();
    }
}
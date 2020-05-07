using MoviesApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesApp.BL.Models
{
    public class PersonDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<PersonActorDetailModel> ActedInMovies { get; set; } = new List<PersonActorDetailModel>();
        public ICollection<PersonDirectorDetailModel> DirectedMovies { get; set; } = new List<PersonDirectorDetailModel>();

        private sealed class PersonDetailModelEqualityComparer : IEqualityComparer<PersonDetailModel>
        {
            public bool Equals(PersonDetailModel x, PersonDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name == y.Name
                       && x.Surname == y.Surname
                       && x.Age == y.Age
                       && x.PictureUrl == y.PictureUrl
                       && x.ActedInMovies.OrderBy(actor => actor.Id).SequenceEqual(
                           y.ActedInMovies.OrderBy(actor => actor.Id),
                           PersonActorDetailModel.PersonActorDetailModelComparer)
                       && x.DirectedMovies.OrderBy(director => director.Id).SequenceEqual(
                           y.DirectedMovies.OrderBy(director => director.Id),
                           PersonDirectorDetailModel.PersonDirectorDetailModelComparer);
            }

            public int GetHashCode(PersonDetailModel obj)
            {
                return HashCode.Combine(obj.Name, obj.Surname, obj.Age, obj.PictureUrl, obj.ActedInMovies, obj.DirectedMovies);
            }
        }

        public static IEqualityComparer<PersonDetailModel> PersonDetailModelComparer { get; } = new PersonDetailModelEqualityComparer();

        private sealed class PersonDetailModelWithoutCollectionsEqualityComparer : IEqualityComparer<PersonDetailModel>
        {
            public bool Equals(PersonDetailModel x, PersonDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name == y.Name 
                       && x.Surname == y.Surname 
                       && x.Age == y.Age 
                       && x.PictureUrl == y.PictureUrl;
            }

            public int GetHashCode(PersonDetailModel obj)
            {
                return HashCode.Combine(obj.Name, obj.Surname, obj.Age, obj.PictureUrl);
            }
        }

        public static IEqualityComparer<PersonDetailModel> PersonDetailModelWithoutCollectionsComparer { get; } = new PersonDetailModelWithoutCollectionsEqualityComparer();
    }
}

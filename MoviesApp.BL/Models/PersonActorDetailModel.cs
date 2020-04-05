using MoviesApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Models
{
    public class PersonActorDetailModel : ModelBase
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }

        private sealed class PersonActorDetailModelEqualityComparer : IEqualityComparer<PersonActorDetailModel>
        {
            public bool Equals(PersonActorDetailModel x, PersonActorDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.ActorId.Equals(y.ActorId)
                       && x.MovieId.Equals(y.MovieId)
                       && string.Equals(x.Name, y.Name)
                       && string.Equals(x.Surname, y.Surname)
                       && x.Age.Equals(y.Age)
                       && string.Equals(x.PictureUrl, y.PictureUrl);
            }

            public int GetHashCode(PersonActorDetailModel obj)
            {
                return HashCode.Combine(obj.ActorId, obj.MovieId, obj.Name, obj.Surname, obj.Age, obj.PictureUrl);
            }
        }

        public static IEqualityComparer<PersonActorDetailModel> PersonActorDetailModelComparer { get; } = new PersonActorDetailModelEqualityComparer();
    }
}
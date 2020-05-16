using System;
using System.Collections.Generic;

namespace MoviesApp.BL.Models
{
    public class PersonActorDetailModel : ModelBase
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }

        private sealed class PersonActorDetailModelEqualityComparer : IEqualityComparer<PersonActorDetailModel>
        {
            public bool Equals(PersonActorDetailModel x, PersonActorDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.ActorId.Equals(y.ActorId)
                       && x.MovieId.Equals(y.MovieId);
            }

            public int GetHashCode(PersonActorDetailModel obj)
            {
                return HashCode.Combine(obj.ActorId, obj.MovieId);
            }
        }

        public static IEqualityComparer<PersonActorDetailModel> PersonActorDetailModelComparer { get; } = new PersonActorDetailModelEqualityComparer();
    }
}
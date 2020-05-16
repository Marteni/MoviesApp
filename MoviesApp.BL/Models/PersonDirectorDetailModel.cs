using System;
using System.Collections.Generic;

namespace MoviesApp.BL.Models
{
    public class PersonDirectorDetailModel : ModelBase
    {
        public Guid DirectorId { get; set; }
        public Guid MovieId { get; set; }

        private sealed class PersonDirectorDetailModelEqualityComparer : IEqualityComparer<PersonDirectorDetailModel>
        {
            public bool Equals(PersonDirectorDetailModel x, PersonDirectorDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.DirectorId.Equals(y.DirectorId)
                       && x.MovieId.Equals(y.MovieId);
            }

            public int GetHashCode(PersonDirectorDetailModel obj)
            {
                return HashCode.Combine(obj.DirectorId, obj.MovieId);
            }
        }

        public static IEqualityComparer<PersonDirectorDetailModel> PersonDirectorDetailModelComparer { get; } = new PersonDirectorDetailModelEqualityComparer();
    }
}
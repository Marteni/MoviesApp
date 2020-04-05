using System;
using System.Collections.Generic;

namespace MoviesApp.BL.Models
{
    public class PersonDirectorDetailModel : ModelBase
    {
        public Guid DirectorId { get; set; }
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }

        private sealed class PersonDirectorDetailModelEqualityComparer : IEqualityComparer<PersonDirectorDetailModel>
        {
            public bool Equals(PersonDirectorDetailModel x, PersonDirectorDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.DirectorId.Equals(y.DirectorId)
                       && x.MovieId.Equals(y.MovieId)
                       && string.Equals(x.Name, y.Name)
                       && string.Equals(x.Surname, y.Surname)
                       && x.Age.Equals(y.Age)
                       && string.Equals(x.PictureUrl, y.PictureUrl);
            }

            public int GetHashCode(PersonDirectorDetailModel obj)
            {
                return HashCode.Combine(obj.DirectorId, obj.MovieId, obj.Name, obj.Surname, obj.Age, obj.PictureUrl);
            }
        }

        public static IEqualityComparer<PersonDirectorDetailModel> PersonDirectorDetailModelComparer { get; } = new PersonDirectorDetailModelEqualityComparer();
    }
}
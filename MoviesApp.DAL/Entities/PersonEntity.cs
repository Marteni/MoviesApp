using System;
using System.Collections.Generic;

namespace MoviesApp.DAL.Entities
{
    public class PersonEntity : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }


        private sealed class PersonEntityEqualityComparer : IEqualityComparer<PersonEntity>
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

        public static IEqualityComparer<PersonEntity> PersonComparer { get; } = new PersonEntityEqualityComparer();
        public static IEqualityComparer<PersonEntity> PersonWithoutCollectionsComparer { get; } = new PersonEntityEqualityComparer();
    }
}
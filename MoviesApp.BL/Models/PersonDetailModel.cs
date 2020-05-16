using System;
using System.Collections.Generic;

namespace MoviesApp.BL.Models
{
    public class PersonDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }

        
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
                       && x.PictureUrl == y.PictureUrl;
            }

            public int GetHashCode(PersonDetailModel obj)
            {
                return HashCode.Combine(obj.Name, obj.Surname, obj.Age, obj.PictureUrl);
            }
        }

        public static IEqualityComparer<PersonDetailModel> PersonDetailModelComparer { get; } = new PersonDetailModelEqualityComparer();
        public static IEqualityComparer<PersonDetailModel> PersonDetailModelWithoutCollectionsComparer { get; } = new PersonDetailModelEqualityComparer();
    }
}

using System;
using System.Collections.Generic;

namespace MoviesApp.DAL.Entities
{
    public class RatingEntity : EntityBase
    {
        public Guid RatedMovie { get; set; }
        public String Nick { get; set; }
        public int NumericEvaluation { get; set; }
        public string Review { get; set; }

        private sealed class RatingEntityEqualityComparer : IEqualityComparer<RatingEntity>
        {
            public bool Equals(RatingEntity x, RatingEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id 
                       && x.RatedMovie.Equals(y.RatedMovie) 
                       && x.Nick == y.Nick 
                       && x.NumericEvaluation == y.NumericEvaluation 
                       && x.Review == y.Review;
            }

            public int GetHashCode(RatingEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.RatedMovie, obj.Nick, obj.NumericEvaluation, obj.Review);
            }
        }

        public static IEqualityComparer<RatingEntity> RatingComparer { get; } = new RatingEntityEqualityComparer();
    }
}
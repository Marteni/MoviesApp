using System;
using System.Collections.Generic;

namespace MoviesApp.DAL.Entities
{
    public class MoviesPersonDirectorEntity : EntityBase
    {
        public Guid DirectorId { get; set; }
        public Guid MovieId { get; set; }
        public PersonEntity Director { get; set; }
        public MovieEntity DirectedMovie { get; set; }

        private sealed class MoviesPersonDirectorEntityEqualityComparer : IEqualityComparer<MoviesPersonDirectorEntity>
        {
            public bool Equals(MoviesPersonDirectorEntity x, MoviesPersonDirectorEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id 
                       && x.DirectorId.Equals(y.DirectorId) 
                       && x.MovieId.Equals(y.MovieId)
                       && PersonEntity.PersonWithoutCollectionsComparer.Equals(x.Director, y.Director)
                       && MovieEntity.MovieWithoutCollectionsComparer.Equals(x.DirectedMovie, y.DirectedMovie);
            }

            public int GetHashCode(MoviesPersonDirectorEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.DirectorId, obj.MovieId, obj.Director, obj.DirectedMovie);
            }
        }

        public static IEqualityComparer<MoviesPersonDirectorEntity> MoviesPersonDirectorEntityComparer { get; } = new MoviesPersonDirectorEntityEqualityComparer();
    }
}
using System;
using System.Collections.Generic;

namespace MoviesApp.DAL.Entities
{
    public class MoviesPersonActorEntity : EntityBase
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        public PersonEntity Actor { get; set; } 
        public MovieEntity ActedInMovie { get; set; }

        private sealed class MoviesPersonActorEntityEqualityComparer : IEqualityComparer<MoviesPersonActorEntity>
        {
            public bool Equals(MoviesPersonActorEntity x, MoviesPersonActorEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id
                       && x.ActorId.Equals(y.ActorId)
                       && x.MovieId.Equals(y.MovieId)
                       && PersonEntity.PersonWithoutCollectionsComparer.Equals(x.Actor, y.Actor)
                       && MovieEntity.MovieWithoutCollectionsComparer.Equals(x.ActedInMovie, y.ActedInMovie);
            }

            public int GetHashCode(MoviesPersonActorEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.ActorId, obj.MovieId, obj.Actor, obj.ActedInMovie);
            }
        }

        public static IEqualityComparer<MoviesPersonActorEntity> MoviesPersonActorEntityComparer { get; } = new MoviesPersonActorEntityEqualityComparer();
    }
} 
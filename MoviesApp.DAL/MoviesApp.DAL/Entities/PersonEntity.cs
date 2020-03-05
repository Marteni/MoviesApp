using System.Collections.Generic;

namespace MoviesApp.DAL.Entities
{
    public class PersonEntity : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        public ICollection<ActedInEntity> ActedInMovies { get; set; } = new List<ActedInEntity>();
        public ICollection<DirectedEntity> DirectedMovies { get; set; } = new List<DirectedEntity>();
        public ICollection<RatingEntity> GivenRatings { get; set; } = new List<RatingEntity>();
    }
}
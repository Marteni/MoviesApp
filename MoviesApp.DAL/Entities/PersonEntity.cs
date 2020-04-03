using System.Collections.Generic;

namespace MoviesApp.DAL.Entities
{
    public class PersonEntity : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<MoviesPersonActorEntity> ActedInMovies { get; set; } = new List<MoviesPersonActorEntity>();
        public ICollection<MoviesPersonDirectorEntity> DirectedMovies { get; set; } = new List<MoviesPersonDirectorEntity>();
        //public ICollection<RatingEntity> GivenRatings { get; set; } = new List<RatingEntity>();
    }
}
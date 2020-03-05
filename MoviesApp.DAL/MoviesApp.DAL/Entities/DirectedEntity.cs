using System;

namespace MoviesApp.DAL.Entities
{
    public class DirectedEntity : EntityBase
    {
        public Guid DirectorId { get; set; }
        public Guid MovieId { get; set; }
        public PersonEntity Director { get; set; }
        public MovieEntity DirectedMovie { get; set; }
    }
}
using System;

namespace MoviesApp.DAL.Entities
{
    public class ActedInEntity : EntityBase
    {
        public Guid ActorId { get; set; }
        public Guid MovieId { get; set; }
        public PersonEntity Actor { get; set; }
        public MovieEntity ActedInMovie { get; set; }
    }
}
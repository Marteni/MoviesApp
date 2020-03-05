using System;

namespace MoviesApp.DAL.Entities
{
    public class RatingEntity : EntityBase
    {
        public Guid Author { get; set; }
        public Guid RatedMovie { get; set; }
        public int NumberRating { get; set; }
        public string WordRating { get; set; }
    }
}
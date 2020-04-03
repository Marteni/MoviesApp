using System;

namespace MoviesApp.DAL.Entities
{
    public class RatingEntity : EntityBase
    {
        public Guid RatedMovie { get; set; }
        public String Nick { get; set; }
        public int NumericEvaluation { get; set; }
        public string Review { get; set; }
    }
}
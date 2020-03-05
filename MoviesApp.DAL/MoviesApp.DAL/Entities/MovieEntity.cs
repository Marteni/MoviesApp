using System;
using System.Collections.Generic;
using MoviesApp.DAL.Enums;

namespace MoviesApp.DAL.Entities
{
    public class MovieEntity : EntityBase
    {
        public string OriginalTitle { get; set; }
        public string CzechTitle { get; set; }
        public GenreType Genre { get; set; }
        public string PosterImage { get; set; }
        public string CountryOfOrigin { get; set; }
        public TimeSpan Length { get; set; }
        public string Description { get; set; }
        public ICollection<ActedInEntity> Actors { get; set; } = new List<ActedInEntity>();
        public ICollection<DirectedEntity> Directors { get; set; } = new List<DirectedEntity>();
        public ICollection<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
    }
}
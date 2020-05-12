using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Enums;

namespace MoviesApp.APP.Wrappers
{
    public class MovieWrapper 
    {
        public Guid Id { get; set; }
        public string OriginalTitle { get; set; }
        public string CzechTitle { get; set; }
        public GenreType Genre { get; set; }
        public string PosterImageUrl { get; set; }
        public string CountryOfOrigin { get; set; }
        public TimeSpan Length { get; set; }
        public string Description { get; set; }
        public ICollection<RatingDetailModel> Ratings { get; set; } = new List<RatingDetailModel>();
        public ICollection<PersonActorDetailModel> Actors { get; set; } = new List<PersonActorDetailModel>();
        public ICollection<PersonDirectorDetailModel> Directors { get; set; } = new List<PersonDirectorDetailModel>();
    }
}

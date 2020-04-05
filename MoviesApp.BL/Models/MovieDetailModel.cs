using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.DAL.Entities;
using MoviesApp.DAL.Enums;

namespace MoviesApp.BL.Models
{
    public class MovieDetailModel: ModelBase
    {
        public string OriginalTitle { get; set; }
        public string CzechTitle { get; set; }
        public GenreType Genre { get; set; }
        public string PosterImageUrl { get; set; }
        public string CountryOfOrigin { get; set; }
        public TimeSpan Length { get; set; }
        public string Description { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Models
{
    public class PersonDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }
    }
}

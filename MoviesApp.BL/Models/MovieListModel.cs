using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Models
{
    public class MovieListModel : ModelBase
    {
        public string Name { get; set; }
        public bool IsActedInChecked { get; set; }
        public bool IsDirectedChecked { get; set; }
    }
}

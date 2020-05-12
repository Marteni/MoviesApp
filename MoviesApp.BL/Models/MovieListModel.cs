using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Models
{
    public class MovieListModel : ModelBase
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}

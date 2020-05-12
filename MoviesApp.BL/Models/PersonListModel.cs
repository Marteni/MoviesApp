using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Models
{
    public class PersonListModel : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsChecked { get; set; }
    }
}

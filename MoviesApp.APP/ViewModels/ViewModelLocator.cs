using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.App.ViewModels;
using MoviesApp.BL.Repositories;
using MoviesApp.DAL.Factories;

namespace MoviesApp.APP.ViewModels
{
    public class ViewModelLocator
    {


        private static MovieRepository _movieRepository = new MovieRepository(new DbContextSqlFactory());

        private static MainViewModel _mainViewModel = new MainViewModel(_movieRepository);
        

        public static MainViewModel MainViewModel
        {

            get
            {
                return _mainViewModel;
            }
        }

      
    }
}

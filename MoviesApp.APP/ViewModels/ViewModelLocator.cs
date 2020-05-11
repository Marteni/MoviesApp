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


        private static readonly MovieRepository _movieRepository = new MovieRepository(new DbContextSqlFactory());

        private static readonly MainViewModel _mainViewModel = new MainViewModel();

        private static readonly MovieListViewModel _movieListViewModel = new MovieListViewModel(_movieRepository);

        private static readonly MovieDetailViewModel _movieDetailViewModel = new MovieDetailViewModel(_movieRepository);


        public static MainViewModel MainViewModel
        {

            get
            {
                return _mainViewModel;
            }
        }

        public static MovieListViewModel MovieListViewModel
        {

            get
            {
                return _movieListViewModel;
            }
        }

        public static MovieDetailViewModel MovieDetailViewModel
        {

            get
            {
                return _movieDetailViewModel;
            }
        }


    }
}

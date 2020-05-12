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
        

        public static MainViewModel MainViewModel { get; } = new MainViewModel();

        public static MovieListViewModel MovieListViewModel { get; } = new MovieListViewModel(_movieRepository);

        public static MovieDetailViewModel MovieDetailViewModel { get; } = new MovieDetailViewModel(_movieRepository);


        public static readonly PeopleRepository _peopleRepository = new PeopleRepository(new DbContextSqlFactory());

        public static PersonListViewModel PersonListViewModel { get; } = new PersonListViewModel(_peopleRepository);

        public static PersonDetailViewModel PersonDetailViewModel { get; } = new PersonDetailViewModel();
    }
}

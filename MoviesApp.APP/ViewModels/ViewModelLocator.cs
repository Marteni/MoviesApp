using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.APP.Services.MessageDialog;
using MoviesApp.App.ViewModels;
using MoviesApp.BL.Repositories;
using MoviesApp.DAL.Factories;

namespace MoviesApp.APP.ViewModels
{
    public class ViewModelLocator
    {


        private static readonly MovieRepository _movieRepository = new MovieRepository(new DbContextSqlFactory());
        private static readonly PeopleRepository _peopleRepository = new PeopleRepository(new DbContextSqlFactory());
        private static readonly MoviePersonActorRepository _actorRepository = new MoviePersonActorRepository(new DbContextSqlFactory());
        private static readonly MoviePersonDirectorRepository _directorRepository = new MoviePersonDirectorRepository(new DbContextSqlFactory());
        private static readonly RatingRepository _ratingRepository = new RatingRepository(new DbContextSqlFactory());
        private static readonly MessageDialogService _messageDialogService = new MessageDialogService();

        public static MainViewModel MainViewModel { get; } = new MainViewModel(_movieRepository,_peopleRepository,_ratingRepository,_messageDialogService);

        public static MovieListViewModel MovieListViewModel { get; } = new MovieListViewModel(_movieRepository);

        public static MovieDetailViewModel MovieDetailViewModel { get; } = new MovieDetailViewModel(_peopleRepository, _actorRepository, _directorRepository, _ratingRepository,_messageDialogService);


       

        public static PersonListViewModel PersonListViewModel { get; } = new PersonListViewModel(_peopleRepository);

        public static PersonDetailViewModel PersonDetailViewModel { get; } = new PersonDetailViewModel(_movieRepository, _actorRepository, _directorRepository,_messageDialogService);
    }
}

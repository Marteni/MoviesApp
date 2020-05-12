using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private IMovieRepository _movieRepository;
        public MovieDetailViewModel(IMovieRepository _movieRepository)
        {
            MovieSaveCommand = new RelayCommand(SaveNewMovie, (canExecute) => true);
            CloseMovieDetailViewCommand = new RelayCommand(CloseMovieDetailView, (canExecute) => true);
            Messenger.Default.Register<MovieAddNewWrapper>(this, OnMovieAddNewReceived,MovieListViewModel.MovieAddToken);
            Messenger.Default.Register<MovieWrapper>(this, OnMovieSelectedReceived, MovieListViewModel.MovieSelectedToken);

        }
     

        public ICommand MovieSaveCommand { get; }
        public ICommand CloseMovieDetailViewCommand { get; }

        private void OnMovieAddNewReceived(MovieAddNewWrapper obj)
        {
            Model = obj;
            if (MovieWrapperDetailModel == null)
            {
                MovieWrapperDetailModel = new MovieWrapper()
                {
                    Id = Model.id
                };
            }

        }

        private void OnMovieSelectedReceived(MovieWrapper obj)
        {
            Model = new MovieAddNewWrapper();
            MovieWrapperDetailModel = obj;
        }


        private void SaveNewMovie(object x = null)
        {
            var movieWrapper = MovieWrapperDetailModel;
            Messenger.Default.Send(movieWrapper, SaveMovieToken);
        }



        private void CloseMovieDetailView(object x = null)
        {
            Model = null;
            MovieWrapperDetailModel = null;
        }

       

        public MovieAddNewWrapper Model { get; set; }

        public MovieWrapper MovieWrapperDetailModel { get; set; }

        public static readonly Guid SaveMovieToken = Guid.Parse("9e8e69dc-7c4f-46c0-8e82-bedce9d9421f");
    }
}

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
            Messenger.Default.Register<MovieAddNewWrapper>(this, OnMovieAddNewReceived);
        }

      

        public ICommand MovieSaveCommand { get; }
        public ICommand CloseMovieDetailViewCommand { get; }

        private void OnMovieAddNewReceived(MovieAddNewWrapper obj)
        {
            Model = obj;

        }

        private void SaveNewMovie(object x = null)
        {
            TestProperty = "cau";
        }

        
        private void CloseMovieDetailView(object x = null)
        {
            Model = null;
        }


        public string TestProperty { get; set; } = "ahoj";

        public MovieAddNewWrapper Model { get; set; }
    }
}

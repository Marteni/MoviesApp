using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.APP.Services;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private MovieAddNewWrapper _model;
        private string _ahoj = "AHOJ";
        private IMovieRepository _movieRepository;
        public MovieDetailViewModel(IMovieRepository _movieRepository)
        {
            Messenger.Default.Register<MovieAddNewWrapper>(this, OnMovieAddNewReceived);
        }

        private void OnMovieAddNewReceived(MovieAddNewWrapper obj)
        {
            _model = obj;
            _ahoj = "cau";
        }

        public string ahoj
        {
            get
            { 
                return _ahoj;
            }
            set
            {
                _ahoj = value;
                OnPropertyChanged(nameof(ahoj));
            }
        }

        public MovieAddNewWrapper Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }
    }
}

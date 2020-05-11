using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieListViewModel : ViewModelBase
    {
        private IMovieRepository _movieRepository;
        public MovieListViewModel(IMovieRepository movieRepository)
        {
            Load(movieRepository);

            MovieDetailCommand = new RelayCommand(AddNewMovie, (canExecute) => true);
        }

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();

        public ICommand MovieDetailCommand { get; }

        private void AddNewMovie(object x = null)
        {
            var newMovieWrapper = new MovieAddNewWrapper
            {
                id = Guid.NewGuid()
            };

            Messenger.Default.Send(newMovieWrapper);
        }

        private void Load(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            var movies = _movieRepository.GetAll();
            Movies.AddRange(movies);
        }
    }
}

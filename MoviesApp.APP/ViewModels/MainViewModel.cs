using System.Collections.Generic;
using System.Collections.ObjectModel;
using MoviesApp.APP.ViewModels;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMovieRepository _movieRepository;
        public MainViewModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            var movies = _movieRepository.GetAll();
            Movies.AddRange(movies);
        }

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();
    }
}

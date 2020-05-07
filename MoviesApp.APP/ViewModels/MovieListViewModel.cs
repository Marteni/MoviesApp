using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieListViewModel : ViewModelBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieListViewModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();

        
    }
}

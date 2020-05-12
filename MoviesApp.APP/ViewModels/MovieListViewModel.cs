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

            MovieDetailCommand = new RelayCommand(AddNewMovieClicked, (canExecute) => true);
            MovieSelectedCommand = new RelayCommand<MovieListModel>(MovieSelected, (canExecute) => true);
            Messenger.Default.Register<MovieWrapper>(this, OnMovieWrapperReceived, MovieDetailViewModel.SaveMovieToken);
        }

        

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();

        public ICommand MovieDetailCommand { get; }

        public ICommand MovieSelectedCommand { get; }

       
        private void AddNewMovieClicked(object x = null)
        {
            var newMovieWrapper = new MovieAddNewWrapper
            {
                id = Guid.NewGuid()
            };
       
            Messenger.Default.Send(newMovieWrapper, MovieAddToken);
        }

        private void MovieSelected(MovieListModel movieListModel)
        {
            var movieDetailViewModel = _movieRepository.GetById(movieListModel.Id);
            var movieSelectedWrapper = new MovieWrapper()
            {
               Id = movieDetailViewModel.Id,
               OriginalTitle = movieDetailViewModel.OriginalTitle,
               CzechTitle = movieDetailViewModel.CzechTitle,
               Genre = movieDetailViewModel.Genre,
               PosterImageUrl = movieDetailViewModel.PosterImageUrl,
               CountryOfOrigin = movieDetailViewModel.CountryOfOrigin,
               Length = movieDetailViewModel.Length,
               Description = movieDetailViewModel.Description,
               Ratings = movieDetailViewModel.Ratings,
               Actors = movieDetailViewModel.Actors,
               Directors = movieDetailViewModel.Directors


            };

            Messenger.Default.Send(movieSelectedWrapper,MovieSelectedToken);

        }
        private void OnMovieWrapperReceived(MovieWrapper MovieWrapperInstance)
        {
            var movieWrapperInstance = MovieWrapperInstance;
            var movieDetailViewModel = new MovieDetailModel()
            {
                Id = movieWrapperInstance.Id,
                OriginalTitle = movieWrapperInstance.OriginalTitle,
                CzechTitle = movieWrapperInstance.CzechTitle,
                Genre = movieWrapperInstance.Genre,
                PosterImageUrl = movieWrapperInstance.PosterImageUrl,
                CountryOfOrigin = movieWrapperInstance.CountryOfOrigin,
                Length = movieWrapperInstance.Length,
                Description = movieWrapperInstance.Description,
                Ratings = movieWrapperInstance.Ratings,
                Actors = movieWrapperInstance.Actors,
                Directors = movieWrapperInstance.Directors

            };

            _movieRepository.Create(movieDetailViewModel);
            Load(_movieRepository);
        }


        private void Load(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            Movies.Clear();
            var movies = _movieRepository.GetAll();
            Movies.AddRange(movies);
        }

        public static readonly Guid MovieAddToken = Guid.Parse("3e5989ad-186a-4262-827c-81569973e36e");
        public static readonly Guid MovieSelectedToken = Guid.Parse("97c53351-898f-45d3-9e55-eaaef9511ed2");
    }
}

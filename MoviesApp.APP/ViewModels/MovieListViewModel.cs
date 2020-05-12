using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
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
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieWrapperNewReceived, MovieDetailViewModel.SaveNewMovieToken);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieWrapperUpdatedReceived, MovieDetailViewModel.UpdateMovieToken);
            Messenger.Default.Register<Guid>(this, OnGuidRecieved, MovieDetailViewModel.DeleteMovieToken);
        }

        private void OnMovieWrapperUpdatedReceived(MovieDetailModel movieDetailModelUpdated)
        {

            _movieRepository.Update(movieDetailModelUpdated);
            Load(_movieRepository);
        }


        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();

        public ICommand MovieDetailCommand { get; }

        public ICommand MovieSelectedCommand { get; }

       
        private void AddNewMovieClicked(object x = null)
        {
            var newMovieModel = new MovieDetailModel()
            {
                Id = Guid.NewGuid()

            };
       
            Messenger.Default.Send(newMovieModel, MovieAddToken);
        }

        private void MovieSelected(MovieListModel movieListModel)
        {
            var movieDetailViewModel = _movieRepository.GetById(movieListModel.Id);

            Messenger.Default.Send(movieDetailViewModel, MovieSelectedToken);

        }
        private void OnMovieWrapperNewReceived(MovieDetailModel movieDetailModel)
        {

            _movieRepository.Create(movieDetailModel);
            Load(_movieRepository);
        }

        private void OnGuidRecieved(Guid id)
        {
            _movieRepository.Delete(id);
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

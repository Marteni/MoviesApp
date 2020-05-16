using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Enums;
using MoviesApp.APP.Services;
using MoviesApp.App.ViewModels;
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
            
            Messenger.Default.Register<MovieDetailModel>(this, NewMoveReceived, MovieDetailViewModel.SaveNewMovieToken);
            Messenger.Default.Register<MovieDetailModel>(this, MovieUpdatedReceived, MovieDetailViewModel.UpdateMovieToken);
            Messenger.Default.Register<Guid>(this, OnGuidReceived, MovieDetailViewModel.DeleteMovieToken);
            Messenger.Default.Register<MovieListModel>(this, SelectedMovieReceived, PersonDetailViewModel.SelectedMovieToken);
        }


        public ICommand MovieDetailCommand { get; }
        public ICommand MovieSelectedCommand { get; }
        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();

        public static readonly Guid MovieAddToken = Guid.Parse("3e5989ad-186a-4262-827c-81569973e36e");
        public static readonly Guid MovieSelectedToken = Guid.Parse("97c53351-898f-45d3-9e55-eaaef9511ed2");


        private void SelectedMovieReceived(MovieListModel selectedMovie)
        {
            var movie = Movies.FirstOrDefault(t => t.Id == selectedMovie.Id);
            MovieSelected(movie);
            var value = (int)TabEnums.MovieTab;
            Messenger.Default.Send(value, MainViewModel.ChangeTabToken);
        }

        private void AddNewMovieClicked(object x = null)
        {
            var newMovieModel = new MovieDetailModel()
            {
                Id = Guid.NewGuid()
            };
       
            Messenger.Default.Send(newMovieModel, MovieAddToken);
        }

        private void NewMoveReceived(MovieDetailModel movieDetailModel)
        {
            _movieRepository.Create(movieDetailModel);
            UpdateMovieListViewWithNewItem(movieDetailModel);
        }

        private void MovieSelected(MovieListModel movieListModel)
        {
            var movieDetailViewModel = _movieRepository.GetById(movieListModel.Id);

            Messenger.Default.Send(movieDetailViewModel, MovieSelectedToken);
        }

        private void MovieUpdatedReceived(MovieDetailModel movieDetailModelUpdated)
        {
            _movieRepository.Update(movieDetailModelUpdated);
            UpdateMovieListWithExistingItem(movieDetailModelUpdated);
        }

        private void OnGuidReceived(Guid id)
        {
            _movieRepository.Delete(id);
            Movies.Remove(Movies.First(t => t.Id == id));
        }

        private void UpdateMovieListViewWithNewItem(MovieDetailModel movieDetailModel)
        {
            var movieListModel = new MovieListModel()
            {
                Id = movieDetailModel.Id,
                Name = movieDetailModel.OriginalTitle
            };
            Movies.Add(movieListModel);
        }

        private void UpdateMovieListWithExistingItem(MovieDetailModel movieDetailModelUpdated)
        {
            var item = Movies.FirstOrDefault(a => a.Id == movieDetailModelUpdated.Id);
            var index = Movies.IndexOf(item);

            if (index != -1)
            {
                Movies[index].Name = movieDetailModelUpdated.OriginalTitle;
                CollectionViewSource.GetDefaultView(Movies).Refresh();
            }
        }

        private void Load(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            Movies.Clear();
            var movies = _movieRepository.GetAll().OrderBy(m=>m.Name);
            Movies.AddRange(movies);
        }
    }
}

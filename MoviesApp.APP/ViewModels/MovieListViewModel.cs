using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Enums;
using MoviesApp.APP.Services;
using MoviesApp.App.ViewModels;
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
            
            Messenger.Default.Register<MovieNewFilledWrapper>(this, NewMoveReceived);
            Messenger.Default.Register<MovieEditWrapper>(this, MovieUpdatedReceived);
            Messenger.Default.Register<MovieDeleteGuidWrapper>(this, DeleteMovieByGuidReceived);
            Messenger.Default.Register<MovieListModel>(this, SelectedMovieReceived);
        }


        public ICommand MovieDetailCommand { get; }
        public ICommand MovieSelectedCommand { get; }
        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();


        private void SelectedMovieReceived(MovieListModel selectedMovie)
        {
            var movie = Movies.FirstOrDefault(t => t.Id == selectedMovie.Id);
            MovieSelected(movie);
            var value = (int)TabEnums.MovieTab;
            Messenger.Default.Send(value);
        }

        private void AddNewMovieClicked(object x = null)
        {
            var newMovieWrapper = new MovieNewWrapper
            {
                Id = Guid.NewGuid()
            };
       
            Messenger.Default.Send(newMovieWrapper);
        }

        private void NewMoveReceived(MovieNewFilledWrapper movieWrapper)
        {
            var movieDetailModel = WrapperMappers.ToMovieDetailModel(movieWrapper);
            _movieRepository.Create(movieDetailModel);
            UpdateMovieListViewWithNewItem(movieDetailModel);
        }

        private void MovieSelected(MovieListModel movieListModel)
        {
            var movieDetailViewModel = _movieRepository.GetById(movieListModel.Id);

            Messenger.Default.Send(WrapperMappers.MovieDetailToMovieSelectedWrapper(movieDetailViewModel));
        }

        private void MovieUpdatedReceived(MovieEditWrapper movieWrapper)
        {
            var movieDetailModelUpdated = WrapperMappers.ToMovieDetailModel(movieWrapper);
            _movieRepository.Update(movieDetailModelUpdated);
            UpdateMovieListWithExistingItem(movieDetailModelUpdated);
        }

        private void DeleteMovieByGuidReceived(MovieDeleteGuidWrapper guidWrapper)
        {
            var id = WrapperMappers.MovieDeleteGuidWrapperToGuid(guidWrapper);
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

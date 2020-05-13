using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class PersonDetailViewModel : ViewModelBase
    {
        private IMovieRepository _movieRepository;
        private IMoviePersonActorRepository _moviesActorRepository;
        private IMoviePersonDirectorRepository _moviesDirectorRepository;
        public PersonDetailViewModel(IMovieRepository movieRepository, IMoviePersonActorRepository moviesActorRepository, IMoviePersonDirectorRepository moviesDirectorRepository)
        {
            _movieRepository = movieRepository;
            _moviesActorRepository = moviesActorRepository;
            _moviesDirectorRepository = moviesDirectorRepository;
            personDetail = personEditDetail = null;
            SavePersonEditViewCommand = new RelayCommand(SavePerson, (canExecute) => true);
            DeletePersonEditViewCommand = new RelayCommand(DeletePerson, (canExecute) => true);
            EditPersonViewCommand = new RelayCommand(EditPerson, (canExecute) => true);

            Messenger.Default.Register<PersonDetailModel>(this, AddNewPerson, PersonListViewModel.AddNewPersonToken);
            Messenger.Default.Register<PersonDetailModel>(this, DisplayPerson, PersonListViewModel.PersonSelectedToken);
        }
        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesActed { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesDirected { get; } = new ObservableCollection<MovieListModel>();
        public ICommand SavePersonEditViewCommand { get; }
        public ICommand DeletePersonEditViewCommand { get; }
        public ICommand EditPersonViewCommand { get; }

        private void AddNewPerson(PersonDetailModel personDetailModel)
        {
            LoadMovies(_movieRepository);
            ExistingPersonFlag = false;
            //if (personDetailModel == null)
            //{
            //    personDetail = null;
            //    return;
            //}
            personDetail = null;
            personEditDetail = new PersonDetailModel();
            {
                personEditDetail.Id = personDetailModel.Id;
            }

            //TODO: Nulovanie kolekcii ActedIn a Directed
        }

        private void DisplayPerson(PersonDetailModel personDetailModel)
        {
            //if (personDetailModel == null)
            //{
            //    personDetail = null;
            //    return;
            //}
            ExistingPersonFlag = true;
            personEditDetail = null;
            personDetail = personDetailModel;
            LoadMovies(_movieRepository);
            LoadActedMovies(_moviesActorRepository);
            //LoadDirectedMovies(_moviesDirectorRepository);
        }

        private void SavePerson(object x = null)
        {
            if (ExistingPersonFlag)
            {
                Messenger.Default.Send(personEditDetail, UpdatePersonToken);
            }
            else
            {
                Messenger.Default.Send(personEditDetail, AddPersonToken);
            }
            personDetail = personEditDetail;
            personEditDetail = null;
            ExistingPersonFlag = false;
            CreateAndReloadMovieActors(_moviesActorRepository);
        }
        private void EditPerson(object x = null)
        {
            ExistingPersonFlag = true;
            personEditDetail = personDetail;
            personDetail = null;
            UpdateMovieListWithActedMovies();

        }
        private void DeletePerson(object x)
        {
            var id = Guid.Parse(personEditDetail.Id.ToString());
            _moviesActorRepository.TryDeleteByActorId(id);
            if (ExistingPersonFlag)
            {
                Messenger.Default.Send(id, DeletePersonToken);
            }
           
            personEditDetail = null;
            ExistingPersonFlag = false;
        }

        
        private void LoadMovies(IMovieRepository movieRepository)
        {
            Movies.Clear();
            _movieRepository = movieRepository;
            var movies = _movieRepository.GetAll();
            Movies.AddRange(movies);
        }

        private void LoadActedMovies(IMoviePersonActorRepository movieActorRepository)
        {
            MoviesActed.Clear();
            _moviesActorRepository = movieActorRepository;
            var movies = _moviesActorRepository.GetAllMovieActorByActorId(personDetail.Id);
            foreach (var movie in movies)
            {
                var actedMovie = _movieRepository.GetByIdListModel(movie.MovieId);
                if (actedMovie != null) MoviesActed.Add(actedMovie);
            }
        }
        private void LoadDirectedMovies(IMoviePersonDirectorRepository movieDirectorRepository)
        {
            MoviesDirected.Clear();
            _moviesDirectorRepository = movieDirectorRepository;
            var movies = _moviesDirectorRepository.GetAllMovieDirectorsByDirectorId(personDetail.Id);
            foreach (var movie in movies)
            {
                var directedMovie = _movieRepository.GetByIdListModel(movie.MovieId);
                if (directedMovie != null) MoviesActed.Add(directedMovie);
            }
        }

        private void CreateAndReloadMovieActors(IMoviePersonActorRepository movieActorRepository)
        {
            foreach (var movie in Movies)
            {
                if (movie.IsChecked)
                {
                    var movieChecked = MoviesActed.FirstOrDefault(x => x.Id == movie.Id);
                    if (movieChecked == null)
                    {
                        var movieActor = new PersonActorDetailModel()
                        {
                            Id = Guid.NewGuid(),
                            MovieId = movie.Id,
                            ActorId = personDetail.Id
                        };

                        movieActorRepository.Create(movieActor);
                        MoviesActed.Add(movie);

                    }
                }
                else
                {
                    var actor = MoviesActed.FirstOrDefault(x => x.Id == movie.Id);
                    if (actor != null)
                    {
                        movieActorRepository.TryDeleteByMovieId(movie.Id);
                        DeleteMovieInMovieListById(movie.Id);
                    }
                }
                LoadActedMovies(movieActorRepository);

            }

        }

        private void DeleteMovieInMovieListById(Guid id)
        {
            var item = MoviesActed.FirstOrDefault(a => a.Id == id);
            var index = MoviesActed.IndexOf(item);

            if (index != -1)
            {
                MoviesActed.RemoveAt(index);

            }
        }

        private void UpdateMovieListWithActedMovies()
        {
            foreach (var movie in Movies)
            {
                var actor = MoviesActed.FirstOrDefault(x => x.Id == movie.Id);
                if (actor != null) movie.IsChecked = true;
            }

            CollectionViewSource.GetDefaultView(Movies).Refresh();
        }

        public bool ExistingPersonFlag { get; set; } = false;
        public PersonDetailModel personDetail { get; set; }
        public PersonDetailModel personEditDetail { get; set; }

        public static readonly Guid AddPersonToken = Guid.Parse("C2C51FFF-64B8-4EEA-9819-3F027C49BE5E");
        public static readonly Guid UpdatePersonToken = Guid.Parse("305EBDDE-72A8-4698-801F-DF49A5313F30");
        public static readonly Guid DeletePersonToken = Guid.Parse("26D8B1E8-033F-47B3-9A8E-36BE53406BF7");
    }
}

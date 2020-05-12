using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        public PersonDetailViewModel(IMovieRepository movieRepository, IMoviePersonActorRepository moviesActorRepository)
        {
            _movieRepository = movieRepository;
            _moviesActorRepository = moviesActorRepository;
            personDetail = personEditDetail = null;
            SavePersonEditViewCommand = new RelayCommand(SavePerson, (canExecute) => true);
            DeletePersonEditViewCommand = new RelayCommand(DeletePerson, (canExecute) => true);
            EditPersonViewCommand = new RelayCommand(EditPerson, (canExecute) => true);

            Messenger.Default.Register<PersonDetailModel>(this, AddNewPerson, PersonListViewModel.AddNewPersonToken);
            Messenger.Default.Register<PersonDetailModel>(this, DisplayPerson, PersonListViewModel.PersonSelectedToken);
        }
        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesActed { get; } = new ObservableCollection<MovieListModel>();
        public ICommand SavePersonEditViewCommand { get; }
        public ICommand DeletePersonEditViewCommand { get; }
        public ICommand EditPersonViewCommand { get; }

        private void AddNewPerson(PersonDetailModel personDetailModel)
        {
            if (personDetailModel == null)
            {
                personDetail = null;
                return;
            }
            personDetail = null;
            personEditDetail = personDetailModel;

            //TODO: Nulovanie kolekcii ActedIn a Directed
        }

        private void DisplayPerson(PersonDetailModel personDetailModel)
        {
            if (personDetailModel == null)
            {
                personDetail = null;
                return;
            }
            ExistingPersonFlag = true;
            personEditDetail = null;
            personDetail = personDetailModel;
            LoadActedMovies(_moviesActorRepository);
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
        }

        private void DeletePerson(object x = null)
        {
            if (ExistingPersonFlag)
            {
                Messenger.Default.Send(Guid.Parse(personEditDetail.Id.ToString()), DeletePersonToken);
            }

            personEditDetail = null;
            ExistingPersonFlag = false;
        }

        private void EditPerson(object x = null)
        {
            ExistingPersonFlag = true;
            personEditDetail = personDetail;
            personDetail = null;
            LoadMovies(_movieRepository);


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

        public bool ExistingPersonFlag { get; set; } = false;
        public PersonDetailModel personDetail { get; set; }
        public PersonDetailModel personEditDetail { get; set; }

        public static readonly Guid AddPersonToken = Guid.Parse("C2C51FFF-64B8-4EEA-9819-3F027C49BE5E");
        public static readonly Guid UpdatePersonToken = Guid.Parse("305EBDDE-72A8-4698-801F-DF49A5313F30");
        public static readonly Guid DeletePersonToken = Guid.Parse("26D8B1E8-033F-47B3-9A8E-36BE53406BF7");
    }
}

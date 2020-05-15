using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Services.MessageDialog;
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
        private readonly IMessageDialogService _messageDialogService;

        public PersonDetailViewModel(IMovieRepository movieRepository, 
            IMoviePersonActorRepository moviesActorRepository, 
            IMoviePersonDirectorRepository moviesDirectorRepository,
            IMessageDialogService messageDialogService)
        {
            _movieRepository = movieRepository;
            _moviesActorRepository = moviesActorRepository;
            _moviesDirectorRepository = moviesDirectorRepository;
            _messageDialogService = messageDialogService;
            personDetail = personEditDetail = null;
            SavePersonEditViewCommand = new RelayCommand(SavePerson, (canExecute) => true);
            DeletePersonEditViewCommand = new RelayCommand(DeletePerson, (canExecute) => true);
            EditPersonViewCommand = new RelayCommand(EditPerson, (canExecute) => true);
            ShowMovieDetailCommand = new RelayCommand<MovieListModel>(ShowMovieDetail, (canExecute) => true);

            Messenger.Default.Register<PersonDetailModel>(this, AddNewPerson, PersonListViewModel.AddNewPersonToken);
            Messenger.Default.Register<PersonDetailModel>(this, DisplayPerson, PersonListViewModel.PersonSelectedToken);
        }

        private void ShowMovieDetail(MovieListModel selectedMovie)
        {
            Messenger.Default.Send(selectedMovie, SelectedMovieToken);
            
        }

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesActed { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesDirected { get; } = new ObservableCollection<MovieListModel>();
        public ICommand SavePersonEditViewCommand { get; }
        public ICommand DeletePersonEditViewCommand { get; }
        public ICommand EditPersonViewCommand { get; }
        public ICommand ShowMovieDetailCommand { get; }

        private void AddNewPerson(PersonDetailModel personDetailModel)
        {
            LoadMovies();
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
            LoadActedMovies(_moviesActorRepository);
            LoadDirectedMovies();
        }

        private void SavePerson(object x = null)
        {
            if (personEditDetail.Name == null || personEditDetail.Surname == null)
            {
                _messageDialogService.Show(
                    "Error",
                    $"Original Name and Surname are empty. Please specify correct name and surname.",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);

                return;
            }

            Messenger.Default.Send(personEditDetail, ExistingPersonFlag ? UpdatePersonToken : AddPersonToken);

            personDetail = personEditDetail;
            personEditDetail = null;
            ExistingPersonFlag = false;

            CreateAndReloadMovieActors(_moviesActorRepository);
            CreateAndReloadMovieDirectors(_moviesDirectorRepository);
        }

        private void EditPerson(object x = null)
        {
           
            ExistingPersonFlag = true;
            personEditDetail = personDetail;
            personDetail = null;

            LoadMovies();
            UpdateMovieListWithActedMovies();
            UpdateMovieListWithDirectedMovies();
        }

        private void DeletePerson(object x)
        {

            var delete = _messageDialogService.Show(
                "Delete",
                $"Do you want to delete {personEditDetail?.Name} {personEditDetail?.Surname}?",
                MessageDialogButtonConfiguration.YesNo,
                MessageDialogResult.No);
            if(delete == MessageDialogResult.No) return;

            var id = Guid.Parse(personEditDetail.Id.ToString());
            
            Messenger.Default.Send(id, DeletePersonToken);

            _moviesActorRepository.TryDeleteAllByMovieOrActorId(id);
            _moviesDirectorRepository.TryDeleteAllByMovieOrDirectorId(id);

            personEditDetail = null;
            
        }

        private void LoadMovies()
        {
            var movies = _movieRepository.GetAll();

            Movies.Clear();
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

        private void CreateAndReloadMovieActors(IMoviePersonActorRepository movieActorRepository)
        {
            foreach (var movie in Movies)
            {
                if (movie.IsActedInChecked)
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
                        movieActorRepository.TryDeleteActorMovieRelation(movie.Id, personDetail.Id);
                        DeleteMovieInActedMovieListById(movie.Id);
                    }
                }

                LoadActedMovies(movieActorRepository);
            }
        }

        private void DeleteMovieInActedMovieListById(Guid id)
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
                if (actor != null) movie.IsActedInChecked = true;
            }

            CollectionViewSource.GetDefaultView(Movies).Refresh();
        }


        private void LoadDirectedMovies()
        {
            MoviesDirected.Clear();
            var movies = _moviesDirectorRepository.GetAllMovieDirectorByDirectorId(personDetail.Id);
            foreach (var movie in movies)
            {
                var directedMovie = _movieRepository.GetByIdListModel(movie.MovieId);
                if (directedMovie != null) MoviesDirected.Add(directedMovie);
            }
        }


        private void CreateAndReloadMovieDirectors(IMoviePersonDirectorRepository movieDirectorRepository)
        {
            foreach (var movie in Movies)
            {
                if (movie.IsDirectedChecked)
                {
                    var movieChecked = MoviesDirected.FirstOrDefault(x => x.Id == movie.Id);
                    if (movieChecked == null)
                    {
                        var movieDirector = new PersonDirectorDetailModel()
                        {
                            Id = Guid.NewGuid(),
                            MovieId = movie.Id,
                            DirectorId = personDetail.Id
                        };

                        movieDirectorRepository.Create(movieDirector);
                        MoviesDirected.Add(movie);
                    }
                }
                else
                {
                    var director = MoviesDirected.FirstOrDefault(x => x.Id == movie.Id);
                    if (director != null)
                    {
                        movieDirectorRepository.TryDeleteDirectorMovieRelation(movie.Id,personDetail.Id);
                        DeleteMovieInDirectedMovieListById(movie.Id);
                    }
                }

                LoadDirectedMovies();
            }
        }

        private void DeleteMovieInDirectedMovieListById(Guid id)
        {
            var item = MoviesDirected.FirstOrDefault(a => a.Id == id);
            var index = MoviesDirected.IndexOf(item);

            if (index != -1)
            {
                MoviesDirected.RemoveAt(index);
            }
        }

        private void UpdateMovieListWithDirectedMovies()
        {
            foreach (var movie in Movies)
            {
                var director = MoviesDirected.FirstOrDefault(x => x.Id == movie.Id);
                if (director != null) movie.IsDirectedChecked = true;
            }

            CollectionViewSource.GetDefaultView(Movies).Refresh();
        }

        public bool ExistingPersonFlag { get; set; } = false;
        public PersonDetailModel personDetail { get; set; }
        public PersonDetailModel personEditDetail { get; set; }

        public static readonly Guid AddPersonToken = Guid.Parse("C2C51FFF-64B8-4EEA-9819-3F027C49BE5E");
        public static readonly Guid UpdatePersonToken = Guid.Parse("305EBDDE-72A8-4698-801F-DF49A5313F30");
        public static readonly Guid DeletePersonToken = Guid.Parse("26D8B1E8-033F-47B3-9A8E-36BE53406BF7");
        public static readonly Guid SelectedMovieToken = Guid.Parse("634c8794-106e-426c-84ee-f825928a3bf5");
    }
}

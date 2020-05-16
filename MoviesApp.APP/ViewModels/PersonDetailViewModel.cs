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
        private readonly IMovieRepository _movieRepository;
        private readonly IMoviePersonActorRepository _moviesActorRepository;
        private readonly IMoviePersonDirectorRepository _moviesDirectorRepository;
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
            PersonDetail = PersonEditDetail = null;

            SavePersonEditViewCommand = new RelayCommand(SavePerson, (canExecute) => true);
            DeletePersonEditViewCommand = new RelayCommand(DeletePerson, (canExecute) => true);
            EditPersonViewCommand = new RelayCommand(EditPerson, (canExecute) => true);
            ShowMovieDetailCommand = new RelayCommand<MovieListModel>(ShowMovieDetail, (canExecute) => true);

            Messenger.Default.Register<PersonDetailModel>(this, AddNewPerson, PersonListViewModel.AddNewPersonToken);
            Messenger.Default.Register<PersonDetailModel>(this, DisplayPerson, PersonListViewModel.PersonSelectedToken);
        }

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesActed { get; } = new ObservableCollection<MovieListModel>();
        public ObservableCollection<MovieListModel> MoviesDirected { get; } = new ObservableCollection<MovieListModel>();

        public ICommand SavePersonEditViewCommand { get; }
        public ICommand DeletePersonEditViewCommand { get; }
        public ICommand EditPersonViewCommand { get; }
        public ICommand ShowMovieDetailCommand { get; }

        public bool ExistingPersonFlag { get; set; } = false;
        public PersonDetailModel PersonDetail { get; set; }
        public PersonDetailModel PersonEditDetail { get; set; }

        public static readonly Guid AddPersonToken = Guid.Parse("C2C51FFF-64B8-4EEA-9819-3F027C49BE5E");
        public static readonly Guid UpdatePersonToken = Guid.Parse("305EBDDE-72A8-4698-801F-DF49A5313F30");
        public static readonly Guid DeletePersonToken = Guid.Parse("26D8B1E8-033F-47B3-9A8E-36BE53406BF7");
        public static readonly Guid SelectedMovieToken = Guid.Parse("634c8794-106e-426c-84ee-f825928a3bf5");


        private void ShowMovieDetail(MovieListModel selectedMovie)
        {
            Messenger.Default.Send(selectedMovie, SelectedMovieToken);
        }

        private void AddNewPerson(PersonDetailModel personDetailModel)
        {
            LoadMovies();
            ExistingPersonFlag = false;
            PersonDetail = null;
            PersonEditDetail = new PersonDetailModel();
            {
                PersonEditDetail.Id = personDetailModel.Id;
            }
        }

        private void DisplayPerson(PersonDetailModel personDetailModel)
        {
            ExistingPersonFlag = true;
            PersonEditDetail = null;
            PersonDetail = personDetailModel;
            LoadActedMovies();
            LoadDirectedMovies();
        }

        private void SavePerson(object x = null)
        {
            if (PersonEditDetail.Name == null || PersonEditDetail.Surname == null)
            {
                _messageDialogService.Show(
                    "Error",
                    $"Original Name and Surname are empty. Please specify correct name and surname.",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);

                return;
            }

            Messenger.Default.Send(PersonEditDetail, ExistingPersonFlag ? UpdatePersonToken : AddPersonToken);

            PersonDetail = PersonEditDetail;
            PersonEditDetail = null;
            ExistingPersonFlag = false;

            CreateAndReloadMovieActors();
            CreateAndReloadMovieDirectors();
        }

        private void EditPerson(object x = null)
        {
            ExistingPersonFlag = true;
            PersonEditDetail = PersonDetail;
            PersonDetail = null;

            LoadMovies();
            UpdateMovieListWithActedMovies();
            UpdateMovieListWithDirectedMovies();
        }

        private void DeletePerson(object x)
        {
            var delete = _messageDialogService.Show(
                "Delete",
                $"Do you want to delete {PersonEditDetail?.Name} {PersonEditDetail?.Surname}?",
                MessageDialogButtonConfiguration.YesNo,
                MessageDialogResult.No);
            if(delete == MessageDialogResult.No) return;

            var id = Guid.Parse(PersonEditDetail.Id.ToString());
            
            Messenger.Default.Send(id, DeletePersonToken);

            _moviesActorRepository.TryDeleteAllByMovieOrActorId(id);
            _moviesDirectorRepository.TryDeleteAllByMovieOrDirectorId(id);

            PersonEditDetail = null;
        }

        private void LoadMovies()
        {
            var movies = _movieRepository.GetAll();

            Movies.Clear();
            Movies.AddRange(movies);
        }

        private void LoadActedMovies()
        {
            MoviesActed.Clear();
            var movies = _moviesActorRepository.GetAllMovieActorByActorId(PersonDetail.Id);
            foreach (var movie in movies)
            {
                var actedMovie = _movieRepository.GetByIdListModel(movie.MovieId);
                if (actedMovie != null) MoviesActed.Add(actedMovie);
            }
        }

        private void CreateAndReloadMovieActors()
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
                            ActorId = PersonDetail.Id
                        };

                        _moviesActorRepository.Create(movieActor);
                        MoviesActed.Add(movie);
                    }
                }
                else
                {
                    var actor = MoviesActed.FirstOrDefault(x => x.Id == movie.Id);
                    if (actor != null)
                    {
                        _moviesActorRepository.TryDeleteActorMovieRelation(movie.Id, PersonDetail.Id);
                        DeleteMovieInActedMovieListById(movie.Id);
                    }
                }

                LoadActedMovies();
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
            var movies = _moviesDirectorRepository.GetAllMovieDirectorByDirectorId(PersonDetail.Id);
            foreach (var movie in movies)
            {
                var directedMovie = _movieRepository.GetByIdListModel(movie.MovieId);
                if (directedMovie != null) MoviesDirected.Add(directedMovie);
            }
        }


        private void CreateAndReloadMovieDirectors()
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
                            DirectorId = PersonDetail.Id
                        };

                        _moviesDirectorRepository.Create(movieDirector);
                        MoviesDirected.Add(movie);
                    }
                }
                else
                {
                    var director = MoviesDirected.FirstOrDefault(x => x.Id == movie.Id);
                    if (director != null)
                    {
                        _moviesDirectorRepository.TryDeleteDirectorMovieRelation(movie.Id,PersonDetail.Id);
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

    }
}

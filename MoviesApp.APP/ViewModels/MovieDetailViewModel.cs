using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Services.MessageDialog;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private IPeopleRepository _personRepository;
        private IMoviePersonActorRepository _movieActorRepository;
        private IMoviePersonDirectorRepository _movieDirectorRepository;
        private IRatingRepository _ratingRepository;
        private readonly IMessageDialogService _messageDialogService;

        public MovieDetailViewModel(IPeopleRepository personRepository,
            IMoviePersonActorRepository movieActorRepository,
            IMoviePersonDirectorRepository movieDirectorRepository,
            IRatingRepository ratingRepository,
            IMessageDialogService messageDialogService
           )
        {
            _personRepository = personRepository;
            _movieActorRepository = movieActorRepository;
            _movieDirectorRepository = movieDirectorRepository;
            _ratingRepository = ratingRepository;
            _messageDialogService = messageDialogService;

            PersonShowDetailCommand = new RelayCommand<PersonListModel>(ShowPersonDetail, (canExecute) => true);
            MovieEditDetailCommand = new RelayCommand(EditMovieDetail, (canExecute) => true);
            MovieSaveCommand = new RelayCommand(SaveNewMovie, (canExecute) => true);
            MovieDeleteDetailCommand = new RelayCommand(DeleteMovieDetail, (canExecute) => CanDeleteFlag);
            MovieCloseDetailViewCommand = new RelayCommand(CloseMovieDetailView, (canExecute) => true);
            RatingShowFormCommand = new RelayCommand(ShowAddRatingForm, canExecute => true);
            RatingSaveNewCommand = new RelayCommand(SaveNewRating, canExecute => true);
            RatingDiscardNewCommand = new RelayCommand(DiscardNewRating, canExecute => true);

            Messenger.Default.Register<MovieDetailModel>(this, OnMovieAddNewReceived,MovieListViewModel.MovieAddToken);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieSelectedReceived, MovieListViewModel.MovieSelectedToken);
        }

        public ICommand PersonShowDetailCommand { get; }
        public ICommand MovieEditDetailCommand { get; }
        public ICommand MovieSaveCommand { get; }
        public ICommand MovieDeleteDetailCommand { get; }
        public ICommand MovieCloseDetailViewCommand { get; }
        public ICommand RatingShowFormCommand { get; }
        public ICommand RatingSaveNewCommand { get; }
        public ICommand RatingDiscardNewCommand { get; }

        public MovieDetailModel Model { get; set; }
        public MovieDetailModel ShowModel { get; set; }
        public MovieDetailModel MovieWrapperDetailModel { get; set; }
        public RatingDetailModel RatingNewDetailModel { get; set; }

        public ObservableCollection<PersonListModel> ActorsEditList { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> DirectorsEditList { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> Actors { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> Directors { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<RatingDetailModel> Ratings { get; set; } = new ObservableCollection<RatingDetailModel>();

        public bool CanDeleteFlag { get; set; }
        public bool CanSaveFlag { get; set; }
        public Visibility ShowRatingAddFormButton { get; set; } = Visibility.Visible;
        public Visibility ShowRatingAddForm { get; set; } = Visibility.Collapsed;
        public double AverageRating { get; set; } = 5.2;

        public static readonly Guid SaveNewMovieToken = Guid.Parse("9e8e69dc-7c4f-46c0-8e82-bedce9d9421f");
        public static readonly Guid DeleteMovieToken = Guid.Parse("c4ba749a-443e-4ea0-8016-e733cdba2275");
        public static readonly Guid UpdateMovieToken = Guid.Parse("34946034-1cc4-4806-988a-60fa608714f7");
        public static readonly Guid SelectedPersonToken = Guid.Parse("aa35fc2f-1bdb-4380-96d6-159ec24603f7");


        private void ShowPersonDetail(PersonListModel selectedPerson)
        {
            Messenger.Default.Send(selectedPerson, SelectedPersonToken);
        }

        private void OnMovieAddNewReceived(MovieDetailModel movieDetailModel)
        {
            LoadPeople();
            CanSaveFlag = true;
            CanDeleteFlag = false;
            ShowModel = null;
            Model = movieDetailModel;
    
            MovieWrapperDetailModel = new MovieDetailModel()
            {
                Id = Model.Id
            };
        }

        private void OnMovieSelectedReceived(MovieDetailModel movieWrapperDetailModel)
        {
            CanDeleteFlag = true;
            ShowModel = new MovieDetailModel();
            Model = null;
            MovieWrapperDetailModel = movieWrapperDetailModel;
            RatingNewDetailModel = new RatingDetailModel();
            LoadActors(_movieActorRepository);
            LoadDirectors();
            LoadRatings();
        }

        private void SaveNewMovie(object x = null)
        {
            var movieWrapper = MovieWrapperDetailModel;
          
            if (CanDeleteFlag)
            {
                Messenger.Default.Send(movieWrapper, UpdateMovieToken );
                Model = null;
                ShowModel = new MovieDetailModel();
            }
            else
            {
                Messenger.Default.Send(movieWrapper, SaveNewMovieToken);
                Model = null;
            }
            
            CreateAndReloadMovieActors(_movieActorRepository);
            CreateAndReloadMovieDirectors(_movieDirectorRepository);
        }

        private void EditMovieDetail(object x = null)
        {
            LoadPeople();
            ShowModel = null;
            Model = new MovieDetailModel();
            UpdatePeopleListWithActors();
            UpdateDirectorListWithDirectors();
        }

        private void DeleteMovieDetail(object obj)
        {
            var delete = _messageDialogService.Show(
                "Delete",
                $"Do you want to delete {MovieWrapperDetailModel?.OriginalTitle}?",
                MessageDialogButtonConfiguration.YesNo,
                MessageDialogResult.No);
            if (delete == MessageDialogResult.No) return;

            var id = Guid.Parse(obj.ToString());

            Messenger.Default.Send(id, DeleteMovieToken);
            _movieActorRepository.TryDeleteAllByMovieOrActorId(MovieWrapperDetailModel.Id);
            _movieDirectorRepository.TryDeleteAllByMovieOrDirectorId(MovieWrapperDetailModel.Id);
            Model = null; 
        }

        private void CloseMovieDetailView(object x = null)
        {
            CanDeleteFlag = false;
            Model = null;
            MovieWrapperDetailModel = null;
        }


        private void LoadPeople()
        {
            var people = _personRepository.GetAll();

            ActorsEditList.Clear();
            ActorsEditList.AddRange(people);

            DirectorsEditList.Clear();
            DirectorsEditList.AddRange(people);
        }

        private void LoadActors(IMoviePersonActorRepository movieActorRepository)
        {
            Actors.Clear();
            _movieActorRepository = movieActorRepository;
            var actors = _movieActorRepository.GetAllMovieActorByMovieId(MovieWrapperDetailModel.Id);
            foreach (var actor in actors)
            {
                var personInCurrentMovie =_personRepository.GetByIdListModel(actor.ActorId);
                if (personInCurrentMovie != null) Actors.Add(personInCurrentMovie);
            }
        }

        private void CreateAndReloadMovieActors(IMoviePersonActorRepository movieActorRepository)
        {
            foreach (var person in ActorsEditList)
            {
                if (person.IsActorChecked)
                {
                    var actor = Actors.FirstOrDefault(x => x.Id == person.Id);
                    if (actor == null)
                    {
                        var movieActor = new PersonActorDetailModel()
                        {
                            Id = Guid.NewGuid(),
                            MovieId = MovieWrapperDetailModel.Id,
                            ActorId = person.Id
                        };

                        movieActorRepository.Create(movieActor);
                        Actors.Add(person);
                    }
                }
                else
                {
                    var actor = Actors.FirstOrDefault(x => x.Id == person.Id);
                    if (actor != null)
                    {
                        movieActorRepository.TryDeleteActorMovieRelation(MovieWrapperDetailModel.Id, person.Id);
                        DeleteActorInActorListById(person.Id);
                    }
                }

                LoadActors(_movieActorRepository);
            }
        }

        private void DeleteActorInActorListById(Guid id)
        {
            var item = Actors.FirstOrDefault(a => a.Id == id);
            var index = Actors.IndexOf(item);

            if (index != -1)
            {
                Actors.RemoveAt(index);
            }
        }

        private void UpdatePeopleListWithActors()
        {
            foreach (var person in ActorsEditList)
            {
                var actor = Actors.FirstOrDefault(x => x.Id == person.Id);
                if (actor != null) person.IsActorChecked = true;
            }

            CollectionViewSource.GetDefaultView(ActorsEditList).Refresh();
        }


        private void LoadDirectors()
        {
            Directors.Clear();
            var directors = _movieDirectorRepository.GetAllMovieDirectorByMovieId(MovieWrapperDetailModel.Id);
            foreach (var director in directors)
            {
                var personInCurrentMovie =_personRepository.GetByIdListModel(director.DirectorId);
                if (personInCurrentMovie != null) Directors.Add(personInCurrentMovie);
            }
        }

        private void CreateAndReloadMovieDirectors(IMoviePersonDirectorRepository movieDirectorRepository)
        {
            foreach (var person in DirectorsEditList)
            {
                if (person.IsDirectorChecked)
                {
                    var director = Directors.FirstOrDefault(x => x.Id == person.Id);
                    if (director == null)
                    {
                        var movieDirector = new PersonDirectorDetailModel()
                        {
                            Id = Guid.NewGuid(),
                            MovieId = MovieWrapperDetailModel.Id,
                            DirectorId = person.Id
                        };

                        movieDirectorRepository.Create(movieDirector);
                        Directors.Add(person);
                    }
                }
                else
                {
                    var director = Directors.FirstOrDefault(x => x.Id == person.Id);
                    if (director != null)
                    {
                        movieDirectorRepository.TryDeleteDirectorMovieRelation(MovieWrapperDetailModel.Id, person.Id);
                        DeleteDirectorInDirectorListById(person.Id);
                    }
                }

                LoadDirectors();
            }
        }

        private void DeleteDirectorInDirectorListById(Guid id)
        {
            var item = Directors.FirstOrDefault(a => a.Id == id);
            var index = Directors.IndexOf(item);

            if (index != -1)
            {
                Directors.RemoveAt(index);
            }
        }

        private void UpdateDirectorListWithDirectors()
        {
            foreach (var person in DirectorsEditList)
            {
                var director = Directors.FirstOrDefault(x => x.Id == person.Id);
                if (director != null) person.IsDirectorChecked = true;
            }

            CollectionViewSource.GetDefaultView(DirectorsEditList).Refresh();
        }

        private void LoadRatings()
        {
            var ratingCounter = 0;
            var ratingValues = 0;

            Ratings.Clear();
            var allRatings = _ratingRepository.GetAllByMovieId(MovieWrapperDetailModel.Id);
            foreach (var rating in allRatings)
            {
                if (rating != null)
                {
                    Ratings.Add(rating);
                    ratingValues += rating.NumericEvaluation;
                    ratingCounter++;
                }
            }

            AverageRating = (double) ratingValues / ratingCounter;
        }

        private void ShowAddRatingForm(object x = null)
        {
            ShowRatingAddFormButton = Visibility.Collapsed;
            ShowRatingAddForm = Visibility.Visible;
        }

        private void SaveNewRating(object x = null)
        {
            RatingNewDetailModel.RatedMovieId = MovieWrapperDetailModel.Id;

            _ratingRepository.Create(RatingNewDetailModel);
            LoadRatings();

            ShowRatingAddFormButton = Visibility.Visible;
            ShowRatingAddForm = Visibility.Collapsed;
            RatingNewDetailModel = new RatingDetailModel();
        }

        private void DiscardNewRating(object x = null)
        {
            ShowRatingAddForm = Visibility.Collapsed;
            ShowRatingAddFormButton = Visibility.Visible;
            RatingNewDetailModel = new RatingDetailModel();
        }
    }
}

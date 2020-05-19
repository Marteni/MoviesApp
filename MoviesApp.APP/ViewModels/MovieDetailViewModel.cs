using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Services.MessageDialog;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private readonly IPeopleRepository _personRepository;
        private readonly IMoviePersonActorRepository _movieActorRepository;
        private readonly IMoviePersonDirectorRepository _movieDirectorRepository;
        private readonly IRatingRepository _ratingRepository;
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

            Messenger.Default.Register<MovieNewWrapper>(this, OnMovieAddNewReceived);
            Messenger.Default.Register<MovieSelectedWrapper>(this, OnMovieSelectedReceived);
        }

        public ICommand PersonShowDetailCommand { get; }
        public ICommand MovieEditDetailCommand { get; }
        public ICommand MovieSaveCommand { get; }
        public ICommand MovieDeleteDetailCommand { get; }
        public ICommand MovieCloseDetailViewCommand { get; }
        public ICommand RatingShowFormCommand { get; }
        public ICommand RatingSaveNewCommand { get; }
        public ICommand RatingDiscardNewCommand { get; }

        public MovieDetailModel DisplayDetailModel { get; set; }
        public MovieDetailModel EditDetailModel { get; set; }
        public RatingDetailModel RatingNewDetailModel { get; set; }

        public ObservableCollection<PersonListModel> ActorsEditList { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> DirectorsEditList { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> Actors { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> Directors { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<RatingDetailModel> Ratings { get; set; } = new ObservableCollection<RatingDetailModel>();

        public bool CanDeleteFlag { get; set; }
        public bool CanSaveFlag { get; set; }
        public Visibility HasRatings { get; set; } = Visibility.Collapsed;
        public Visibility DoesntHaveRatings { get; set; } = Visibility.Visible;
        public Visibility ShowRatingAddFormButton { get; set; } = Visibility.Visible;
        public Visibility ShowRatingAddForm { get; set; } = Visibility.Collapsed;
        public double AverageRating { get; set; }


        private void ShowPersonDetail(PersonListModel selectedPerson)
        {
            Messenger.Default.Send(selectedPerson);
        }

        private void OnMovieAddNewReceived(MovieNewWrapper movieDetailModel)
        {
            LoadPeople();
            CanSaveFlag = true;
            CanDeleteFlag = false;
            DisplayDetailModel = null;
            EditDetailModel = new MovieDetailModel
            {
                Id = movieDetailModel.Id
            };
        }

        private void OnMovieSelectedReceived(MovieSelectedWrapper movieWrapper)
        {
            CanDeleteFlag = true;
            DisplayDetailModel = WrapperMappers.ToMovieDetailModel(movieWrapper);
            EditDetailModel = null;
            RatingNewDetailModel = new RatingDetailModel();
            LoadActors();
            LoadDirectors();
            LoadRatings();
        }

        private void SaveNewMovie(object x = null)
        {
            var movieWrapper = EditDetailModel;

            if (String.IsNullOrEmpty(movieWrapper.OriginalTitle))
            {
                    _messageDialogService.Show(
                    "Error",
                    $"Original Title is empty. Please specify title of movie.",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);

                return;
            }

            if (CanDeleteFlag)
            {
                Messenger.Default.Send(WrapperMappers.MovieDetailToMovieEditWrapper(movieWrapper));
            }
            else
            {
                Messenger.Default.Send(WrapperMappers.MovieDetailToMovieNewFilledWrapper(movieWrapper));
            }

            DisplayDetailModel = EditDetailModel;
            CreateAndReloadMovieActors();
            CreateAndReloadMovieDirectors();

           
            EditDetailModel = null;
            CanDeleteFlag = false;
        }

        private void EditMovieDetail(object x = null)
        {
            LoadPeople();
            CanDeleteFlag = true;
            EditDetailModel = DisplayDetailModel;
            DisplayDetailModel = null;
            
            UpdatePeopleListWithActors();
            UpdateDirectorListWithDirectors();
        }

        private void DeleteMovieDetail(object obj)
        {
            var delete = _messageDialogService.Show(
                "Delete",
                $"Do you want to delete {EditDetailModel?.OriginalTitle}?",
                MessageDialogButtonConfiguration.YesNo,
                MessageDialogResult.No);
            if (delete == MessageDialogResult.No) return;

            var id = Guid.Parse(obj.ToString());

            Messenger.Default.Send(WrapperMappers.GuidToMovieDeleteGuidWrapper(id));
            _movieActorRepository.TryDeleteAllByMovieOrActorId(EditDetailModel.Id);
            _movieDirectorRepository.TryDeleteAllByMovieOrDirectorId(EditDetailModel.Id);
            EditDetailModel = null; 
        }

        private void CloseMovieDetailView(object x = null)
        {
            CanDeleteFlag = false;
            DisplayDetailModel = null;
            EditDetailModel = null;
        }


        private void LoadPeople()
        {
            var people = _personRepository.GetAll();

            ActorsEditList.Clear();
            ActorsEditList.AddRange(people);

            DirectorsEditList.Clear();
            DirectorsEditList.AddRange(people);
        }

        private void LoadActors()
        {
            Actors.Clear();
            var actors = _movieActorRepository.GetAllMovieActorByMovieId(DisplayDetailModel.Id);
            foreach (var actor in actors)
            {
                var personInCurrentMovie =_personRepository.GetByIdListModel(actor.ActorId);
                if (personInCurrentMovie != null) Actors.Add(personInCurrentMovie);
            }
        }

        private void CreateAndReloadMovieActors()
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
                            MovieId = EditDetailModel.Id,
                            ActorId = person.Id
                        };

                        _movieActorRepository.Create(movieActor);
                        Actors.Add(person);
                    }
                }
                else
                {
                    var actor = Actors.FirstOrDefault(x => x.Id == person.Id);
                    if (actor != null)
                    {
                        _movieActorRepository.TryDeleteActorMovieRelation(EditDetailModel.Id, person.Id);
                        DeleteActorInActorListById(person.Id);
                    }
                }

                LoadActors();
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

        }


        private void LoadDirectors()
        {
            Directors.Clear();
            var directors = _movieDirectorRepository.GetAllMovieDirectorByMovieId(DisplayDetailModel.Id);
            foreach (var director in directors)
            {
                var personInCurrentMovie =_personRepository.GetByIdListModel(director.DirectorId);
                if (personInCurrentMovie != null) Directors.Add(personInCurrentMovie);
            }
        }

        private void CreateAndReloadMovieDirectors()
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
                            MovieId = EditDetailModel.Id,
                            DirectorId = person.Id
                        };

                        _movieDirectorRepository.Create(movieDirector);
                        Directors.Add(person);
                    }
                }
                else
                {
                    var director = Directors.FirstOrDefault(x => x.Id == person.Id);
                    if (director != null)
                    {
                        _movieDirectorRepository.TryDeleteDirectorMovieRelation(EditDetailModel.Id, person.Id);
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

        }


        private void LoadRatings()
        {
            var ratingCounter = 0;
            var ratingValues = 0;

            Ratings.Clear();
            var allRatings = _ratingRepository.GetAllByMovieId(DisplayDetailModel.Id);
            foreach (var rating in allRatings)
            {
                if (rating != null)
                {
                    Ratings.Add(rating);
                    ratingValues += rating.NumericEvaluation;
                    ratingCounter++;
                }
            }

            if (ratingCounter != 0)
            {
                AverageRating = (double) ratingValues / ratingCounter;
                HasRatings = Visibility.Visible;
                DoesntHaveRatings = Visibility.Collapsed;
            }
            else
            {
                AverageRating = 0;
                HasRatings = Visibility.Collapsed;
                DoesntHaveRatings = Visibility.Visible;
            }
        }

        private void ShowAddRatingForm(object x = null)
        {
            ShowRatingAddFormButton = Visibility.Collapsed;
            ShowRatingAddForm = Visibility.Visible;
        }

        private void SaveNewRating(object x = null)
        {
            if (RatingNewDetailModel.Nick == null || RatingNewDetailModel.NumericEvaluation <= 0)
            {
                _messageDialogService.Show(
                    "Error",
                    $"Please specify correct nick and numerical rating.",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);

                return;
            }
            RatingNewDetailModel.RatedMovieId = EditDetailModel.Id;

            _ratingRepository.Create(RatingNewDetailModel);
            LoadRatings();
            DiscardNewRating();
        }

        private void DiscardNewRating(object x = null)
        {
            ShowRatingAddForm = Visibility.Collapsed;
            ShowRatingAddFormButton = Visibility.Visible;
            RatingNewDetailModel = new RatingDetailModel();
        }
    }
}

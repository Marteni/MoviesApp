using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    public class MovieDetailViewModel : ViewModelBase
    {
        private IPeopleRepository _personRepository;
        private IMoviePersonActorRepository _actorRepository;
        private IMovieRepository _movieRepository;
        public MovieDetailViewModel(IPeopleRepository personRepository,
            IMoviePersonActorRepository actorRepository,
            IMovieRepository movieRepository)
        {
            _personRepository = personRepository;
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            MovieSaveCommand = new RelayCommand(SaveNewMovie, (canExecute) => true);
            CloseMovieDetailViewCommand = new RelayCommand(CloseMovieDetailView, (canExecute) => true);
            EditMovieDetailCommand = new RelayCommand(EditMovieDetail, (canExecute) => true);
            DeleteMovieDetailCommand = new RelayCommand(DeleteMovieDetail, (canExecute) => CanDeleteFlag);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieAddNewReceived,MovieListViewModel.MovieAddToken);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieSelectedReceived, MovieListViewModel.MovieSelectedToken);

           

        }

        public ObservableCollection<PersonListModel> People { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> Actors { get; } = new ObservableCollection<PersonListModel>();

        private void EditMovieDetail(object x = null)
        {
            ShowModel = null;
            Model = new MovieDetailModel();
            LoadPeople(_personRepository);
            
        }


        public ICommand MovieSaveCommand { get; }
        public ICommand CloseMovieDetailViewCommand { get; }
        public ICommand DeleteMovieDetailCommand { get; }
        public ICommand EditMovieDetailCommand { get; }

        private void OnMovieAddNewReceived(MovieDetailModel movieDetailModel)
        {
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
            LoadActors(_actorRepository);
        }


        private void SaveNewMovie(object x = null)
        {
            var movieWrapper = MovieWrapperDetailModel;
          
            if (CanDeleteFlag)
            {
                Messenger.Default.Send(movieWrapper, UpdateMovieToken );
            }
            else
            {
                Messenger.Default.Send(movieWrapper, SaveNewMovieToken);
                Model = null;
            }
                
           
        }



        private void CloseMovieDetailView(object x = null)
        {
            CanDeleteFlag = false;
            Model = null;
            MovieWrapperDetailModel = null;
        }

        private void DeleteMovieDetail(object obj)
        {
            var id = Guid.Parse(obj.ToString());

            Messenger.Default.Send(id, DeleteMovieToken);
            Model = null; 
        }



        public bool CanDeleteFlag { get; set; }
        public bool CanSaveFlag { get; set; }
        public MovieDetailModel Model { get; set; }

        public MovieDetailModel ShowModel { get; set; }

        public MovieDetailModel MovieWrapperDetailModel { get; set; }


        private void LoadPeople(IPeopleRepository personRepository)
        {
            People.Clear();
            _personRepository = personRepository;
            var people = _personRepository.GetAll();
            foreach (var person in people)
            {
                var actor = Actors.FirstOrDefault(x => x.Id == person.Id);
                if (actor != null) person.IsChecked = true;
                People.Add(person);
                
            }
       
            
        }

        private void LoadActors(IMoviePersonActorRepository actorRepository)
        {
            Actors.Clear();
            _actorRepository = actorRepository;
            var actors = _actorRepository.GetAllMovieActorByMovieId(MovieWrapperDetailModel.Id);
            foreach (var actor in actors)
            {
                var personInCurrentMovie =_personRepository.GetByIdListModel(actor.ActorId);
                if (personInCurrentMovie != null) Actors.Add(personInCurrentMovie);
            }
           
        }

        public static readonly Guid SaveNewMovieToken = Guid.Parse("9e8e69dc-7c4f-46c0-8e82-bedce9d9421f");
        public static readonly Guid DeleteMovieToken = Guid.Parse("c4ba749a-443e-4ea0-8016-e733cdba2275");
        public static readonly Guid UpdateMovieToken = Guid.Parse("34946034-1cc4-4806-988a-60fa608714f7");
    }
}

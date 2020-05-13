using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class MovieDetailViewModel : ViewModelBase
    {
        private IPeopleRepository _personRepository;
        private IMoviePersonActorRepository _movieActorRepository;
        private IMovieRepository _movieRepository;
        private IMoviePersonDirectorRepository _directorRepository;
        public MovieDetailViewModel(IPeopleRepository personRepository,
            IMoviePersonActorRepository movieActorRepository,
            IMoviePersonDirectorRepository directorRepository,
            IMovieRepository movieRepository
           )
        {
            _personRepository = personRepository;
            _movieActorRepository = movieActorRepository;
            _movieRepository = movieRepository;
            _directorRepository = directorRepository;

            
            

            MovieSaveCommand = new RelayCommand(SaveNewMovie, (canExecute) => true);
            CloseMovieDetailViewCommand = new RelayCommand(CloseMovieDetailView, (canExecute) => true);
            EditMovieDetailCommand = new RelayCommand(EditMovieDetail, (canExecute) => true);
            DeleteMovieDetailCommand = new RelayCommand(DeleteMovieDetail, (canExecute) => CanDeleteFlag);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieAddNewReceived,MovieListViewModel.MovieAddToken);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieSelectedReceived, MovieListViewModel.MovieSelectedToken);

           

        }

        public ObservableCollection<PersonListModel> People { get; } = new ObservableCollection<PersonListModel>();
        public ObservableCollection<PersonListModel> Actors { get; } = new ObservableCollection<PersonListModel>();

       
       


        public ICommand MovieSaveCommand { get; }
        public ICommand CloseMovieDetailViewCommand { get; }
        public ICommand DeleteMovieDetailCommand { get; }
        public ICommand EditMovieDetailCommand { get; }

        private void OnMovieAddNewReceived(MovieDetailModel movieDetailModel)
        {
            LoadPeople(_personRepository);
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
            LoadPeople(_personRepository);
            LoadActors(_movieActorRepository);

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


        }

        private void EditMovieDetail(object x = null)
        {

            ShowModel = null;
            Model = new MovieDetailModel();
            UpdatePeopleListWithActors();
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
            _movieActorRepository.TryDeleteByMovieId(id);
            Model = null; 
        }




        private void LoadPeople(IPeopleRepository personRepository)
        {
            People.Clear();
            _personRepository = personRepository;
            var people = _personRepository.GetAll();
            People.AddRange(people);

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
            foreach (var person in People)
            {
                if (person.IsChecked)
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
                        movieActorRepository.TryDeleteByActorId(person.Id);
                        DeleteActorInActorListById(person.Id);
                    }
                }

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
            foreach (var person in People)
            {
                var actor = Actors.FirstOrDefault(x => x.Id == person.Id);
                if (actor != null) person.IsChecked = true;
            }

            CollectionViewSource.GetDefaultView(People).Refresh();
        }
        public bool CanDeleteFlag { get; set; }
        public bool CanSaveFlag { get; set; }
        public MovieDetailModel Model { get; set; }

        public MovieDetailModel ShowModel { get; set; }

        public MovieDetailModel MovieWrapperDetailModel { get; set; }

        public static readonly Guid SaveNewMovieToken = Guid.Parse("9e8e69dc-7c4f-46c0-8e82-bedce9d9421f");
        public static readonly Guid DeleteMovieToken = Guid.Parse("c4ba749a-443e-4ea0-8016-e733cdba2275");
        public static readonly Guid UpdateMovieToken = Guid.Parse("34946034-1cc4-4806-988a-60fa608714f7");
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Enums;
using MoviesApp.APP.Services;
using MoviesApp.APP.ViewModels;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private IMovieRepository _movieRepository;
        private IPeopleRepository _peopleRepository;

        public MainViewModel(IMovieRepository movieRepository,IPeopleRepository peopleRepository)
        {
            _movieRepository = movieRepository;
            _peopleRepository = peopleRepository;
            SearchCommand = new RelayCommand(OnSearch, (canExecute) => true);
            CloseSearchViewMoviesCommand = new RelayCommand<MovieDetailModel>(OnCloseSearchMoviesView, (canExecute) => true);
            CloseSearchViewPeopleCommand = new RelayCommand<PersonListModel>(OnCloseSearchPeopleView, (canExecute) => true);
            CloseSearchViewCommand = new RelayCommand(OnClose, (canExecute) => true);
            Messenger.Default.Register<int>(this, OnTabReceived,ChangeTabToken);
        }

       


        public string SearchQuery { get; set; }
        public int TabItem { get; set; }
        public bool ToggleSearchView { get; set; } = false;
        public bool ToggleTabView { get; set; } = true;
        public IList<MovieDetailModel> FoundMovies { get; set; } = new List<MovieDetailModel>();
        public IList<PersonListModel> FoundPeople { get; set; } = new List<PersonListModel>();
        public ICommand SearchCommand { get; }
        public ICommand CloseSearchViewCommand { get; }
        public ICommand CloseSearchViewMoviesCommand { get; }
        public ICommand CloseSearchViewPeopleCommand { get; }
        private void OnClose(object obj)
        {
            ToggleSearchView = false;
            ToggleTabView = true;
            SearchQuery = null;
        }
        private void OnCloseSearchMoviesView(MovieDetailModel selectedItem)
        {
            
            var listModel = new MovieListModel()
            {
                Id = selectedItem.Id
            };
            Messenger.Default.Send(listModel, PersonDetailViewModel.SelectedMovieToken);
            
            ToggleSearchView = false;
            ToggleTabView = true;
            SearchQuery = null;
        }
        private void OnCloseSearchPeopleView(object selectedItem)
        {
           
            Messenger.Default.Send((PersonListModel)selectedItem, MovieDetailViewModel.SelectedPersonToken);
            ToggleSearchView = false;
            ToggleTabView = true;
            SearchQuery = null;
        }

        private void OnTabReceived(int tabIndex)
        {
            TabItem = tabIndex;
        }

        private void OnSearch(object obj)
        {
            
          
            if (SearchQuery != null)
            {
                
                Regex searchTerm = new Regex(SearchQuery, RegexOptions.IgnoreCase);
                
                var allMovies = _movieRepository.GetAllDetails();
                FoundMovies.Clear();
                FoundMovies = allMovies.Where(x => searchTerm.IsMatch(x.OriginalTitle) 
                                                   || searchTerm.IsMatch(x.CzechTitle)
                                                   || searchTerm.IsMatch(x.CountryOfOrigin)
                                                   || searchTerm.IsMatch(x.Description)).ToList();

                var allPeople = _peopleRepository.GetAll();
                FoundPeople.Clear();
                FoundPeople = allPeople.Where(x => searchTerm.IsMatch(x.Name)
                                                   || searchTerm.IsMatch(x.Surname)).ToList();

            }

           
            ToggleTabView = false;
            ToggleSearchView = true;
        }
    

      

        public static readonly Guid ChangeTabToken = Guid.Parse("be54ef43-fb66-4528-a558-b8ef69453fee");
    }
}

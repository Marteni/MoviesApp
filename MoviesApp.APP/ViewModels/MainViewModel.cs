using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Services.MessageDialog;
using MoviesApp.APP.ViewModels;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IRatingRepository _ratingRepository;

        public MainViewModel(IMovieRepository movieRepository,
            IPeopleRepository peopleRepository, 
            IRatingRepository ratingRepository)
        {
            _movieRepository = movieRepository;
            _peopleRepository = peopleRepository;
            _ratingRepository = ratingRepository;

            SearchCommand = new RelayCommand(OnSearch, (canExecute) => true);
            CloseSearchViewMoviesCommand = new RelayCommand<MovieDetailModel>(OnCloseSearchMoviesView, (canExecute) => true);
            CloseSearchViewPeopleCommand = new RelayCommand<PersonListModel>(OnCloseSearchPeopleView, (canExecute) => true);
            CloseSearchViewRatingsCommand = new RelayCommand<RatingDetailModel>(OnCloseSearchRatingsView, (canExecute) => true);
            CloseSearchViewCommand = new RelayCommand(OnClose, (canExecute) => true);
            
            Messenger.Default.Register<int>(this, OnTabReceived,ChangeTabToken);
        }

        public IList<MovieDetailModel> FoundMovies { get; set; } = new List<MovieDetailModel>();
        public IList<PersonListModel> FoundPeople { get; set; } = new List<PersonListModel>();
        public IList<RatingDetailModel> FoundRatings { get; set; } = new List<RatingDetailModel>();

        public ICommand SearchCommand { get; }
        public ICommand CloseSearchViewCommand { get; }
        public ICommand CloseSearchViewMoviesCommand { get; }
        public ICommand CloseSearchViewPeopleCommand { get; }
        public ICommand CloseSearchViewRatingsCommand { get; }

        public int TabItem { get; set; }
        public bool ToggleSearchView { get; set; } = false;
        public bool ToggleTabView { get; set; } = true;
        private string _searchQueryText;
        public string SearchQuery
        {
            get { return _searchQueryText; }
            set
            {
                _searchQueryText = value;
                SearchCommand.Execute(null);
            }
        }

        public static readonly Guid ChangeTabToken = Guid.Parse("be54ef43-fb66-4528-a558-b8ef69453fee");


        private void OnCloseSearchRatingsView(RatingDetailModel ratingDetail)
        {
            var listModel = new MovieListModel()
            {
                Id = ratingDetail.RatedMovieId
            };

            Messenger.Default.Send(listModel, PersonDetailViewModel.SelectedMovieToken);

            ToggleSearchView = false;
            ToggleTabView = true;
            SearchQuery = null;
        }

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

            if (string.IsNullOrEmpty(SearchQuery))
            {
                OnClose(null);
                return;
            }
            
            var searchQuery = Regex.Replace(SearchQuery, @"\s+", @"\s*");
            Regex searchTerm = new Regex(searchQuery, RegexOptions.IgnoreCase);


            var allMovies = _movieRepository.GetAllDetails();
            FoundMovies.Clear();
            FoundMovies = allMovies.Where(x => searchTerm.IsMatch(x.OriginalTitle ?? (x.OriginalTitle = "") ) 
                                               || searchTerm.IsMatch(x.CzechTitle ?? (x.CzechTitle = ""))
                                               || searchTerm.IsMatch(x.CountryOfOrigin ?? (x.CountryOfOrigin = ""))
                                               || searchTerm.IsMatch(x.Description ?? (x.Description = ""))).ToList();

            var allPeople = _peopleRepository.GetAll();
            FoundPeople.Clear();
            FoundPeople = allPeople.Where(x => searchTerm.IsMatch((x.Name + " " + x.Surname)))
                .ToList();

            var allRatings = _ratingRepository.GetAll();
            FoundRatings.Clear();
            FoundRatings = allRatings.Where(x => searchTerm.IsMatch(x.Review ?? (x.Review = "")))
                .ToList();

            ToggleTabView = false;
            ToggleSearchView = true;
        }
    }
}

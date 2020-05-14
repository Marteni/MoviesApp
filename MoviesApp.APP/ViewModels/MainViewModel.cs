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

        public MainViewModel(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            SearchCommand = new RelayCommand(OnSearch, (canExecute) => true);
            Messenger.Default.Register<int>(this, OnTabReceived,ChangeTabToken);
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
                var serac = searchTerm.IsMatch("Star Wars episode 5 return of Jedi");
                var allMovies = _movieRepository.GetAllDetails();
                FoundMovies.Clear();
                FoundMovies = allMovies.Where(x => searchTerm.IsMatch(x.OriginalTitle)
                                                   || searchTerm.IsMatch(x.CzechTitle)
                                                   || searchTerm.IsMatch(x.CountryOfOrigin)
                                                   || searchTerm.IsMatch(x.Description)).ToList();
            }
        }

        public string SearchQuery { get; set; }
        public int TabItem { get; set; }
        public IList<MovieDetailModel> FoundMovies { get; set; } = new List<MovieDetailModel>();
        public ICommand SearchCommand { get; }

        public static readonly Guid ChangeTabToken = Guid.Parse("be54ef43-fb66-4528-a558-b8ef69453fee");
    }
}

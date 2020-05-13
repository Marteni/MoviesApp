using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.ViewModels;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        

        public MainViewModel()
        {
            SearchCommand = new RelayCommand(OnSearch, (canExecute) => true);
        }

        private void OnSearch(object obj)
        {
            throw new System.NotImplementedException();
        }

        public string SearchQuery { get; set; }

        public ICommand SearchCommand { get; }

    }
}

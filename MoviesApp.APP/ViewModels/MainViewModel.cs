using System.Collections.Generic;
using System.Collections.ObjectModel;
using MoviesApp.APP.Services;
using MoviesApp.APP.ViewModels;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        

        public MainViewModel()
        {
           
        }

     

        public ObservableCollection<MovieListModel> Movies { get; } = new ObservableCollection<MovieListModel>();

      
    }
}

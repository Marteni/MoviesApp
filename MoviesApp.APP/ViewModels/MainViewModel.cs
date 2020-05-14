using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        

        public MainViewModel()
        {
            SearchCommand = new RelayCommand(OnSearch, (canExecute) => true);
            Messenger.Default.Register<int>(this, OnTabReceived,ChangeTabToken);
        }

        private void OnTabReceived(int tabIndex)
        {
            TabItem = tabIndex;
        }

        private void OnSearch(object obj)
        {
            throw new System.NotImplementedException();
        }

        public string SearchQuery { get; set; }
        public int TabItem { get; set; }

        public ICommand SearchCommand { get; }

        public static readonly Guid ChangeTabToken = Guid.Parse("be54ef43-fb66-4528-a558-b8ef69453fee");
    }
}

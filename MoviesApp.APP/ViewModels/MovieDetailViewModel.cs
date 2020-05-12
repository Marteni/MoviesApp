using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private IMovieRepository _movieRepository;
        public MovieDetailViewModel(IMovieRepository _movieRepository)
        {
            MovieSaveCommand = new RelayCommand(SaveNewMovie, (canExecute) => true);
            CloseMovieDetailViewCommand = new RelayCommand(CloseMovieDetailView, (canExecute) => true);
            DeleteMovieDetailCommand = new RelayCommand(DeleteMovieDetail, (canExecute) => CanDeleteFlag);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieAddNewReceived,MovieListViewModel.MovieAddToken);
            Messenger.Default.Register<MovieDetailModel>(this, OnMovieSelectedReceived, MovieListViewModel.MovieSelectedToken);

        }

       

        public ICommand MovieSaveCommand { get; }
        public ICommand CloseMovieDetailViewCommand { get; }
        public ICommand DeleteMovieDetailCommand { get; }

        private void OnMovieAddNewReceived(MovieDetailModel movieDetailModel)
        {
            CanSaveFlag = true;
            CanDeleteFlag = false;
            Model = movieDetailModel;
    
            MovieWrapperDetailModel = new MovieDetailModel()
            {
                Id = Model.Id
            };
            

        }

        private void OnMovieSelectedReceived(MovieDetailModel movieWrapperDetailModel)
        {
            CanDeleteFlag = true;
            Model = new MovieDetailModel();
            MovieWrapperDetailModel = movieWrapperDetailModel;
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

        public MovieDetailModel MovieWrapperDetailModel { get; set; }

        public static readonly Guid SaveNewMovieToken = Guid.Parse("9e8e69dc-7c4f-46c0-8e82-bedce9d9421f");
        public static readonly Guid DeleteMovieToken = Guid.Parse("c4ba749a-443e-4ea0-8016-e733cdba2275");
        public static readonly Guid UpdateMovieToken = Guid.Parse("34946034-1cc4-4806-988a-60fa608714f7");
    }
}

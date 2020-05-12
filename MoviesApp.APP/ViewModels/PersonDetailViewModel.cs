using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class PersonDetailViewModel : ViewModelBase
    {
        private IPeopleRepository _peopleRepository;
        public PersonDetailViewModel(IPeopleRepository _peopleRepository)
        {
            personDetail = personEditDetail = null;
            SavePersonEditViewCommand = new RelayCommand(SavePerson, (canExecute) => true);
            DeletePersonEditViewCommand = new RelayCommand(SavePerson, (canExecute) => true);
            EditPersonViewCommand = new RelayCommand(EditPerson, (canExecute) => true);
            Messenger.Default.Register<PersonDetailModel>(this, DisplayPerson, PersonListViewModel.PersonSelectedToken);
        }
        public ICommand SavePersonEditViewCommand { get; }
        public ICommand DeletePersonEditViewCommand { get; }
        public ICommand EditPersonViewCommand { get; }

        private void DisplayPerson(PersonDetailModel personDetailModel = null)
        {
            if (personDetailModel == null)
            {
                personDetail = null;
                return;
            }

            personEditDetail = null;
            personDetail = personDetailModel;
        }
        private void SavePerson(object x = null)
        {
            personDetail = personEditDetail;
            personEditDetail = null;
        }

        private void EditPerson(object x = null)
        {
            personEditDetail = personDetail;
            personDetail = null;
        }

        public PersonDetailModel personDetail { get; set; }
        public PersonDetailModel personEditDetail { get; set; }

        public PersonAddNewWrapper Model { get; set; }
    }
}

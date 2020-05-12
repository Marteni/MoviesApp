using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Services;
using MoviesApp.APP.Wrappers;
using MoviesApp.BL.Extensions;
using MoviesApp.BL.Models;
using MoviesApp.BL.Repositories;

namespace MoviesApp.APP.ViewModels
{
    public class PersonListViewModel : ViewModelBase
    {
        private IPeopleRepository _personRepository;

        public PersonListViewModel(IPeopleRepository personRepository)
        {
            Load(personRepository);

            PersonDetailCommand = new RelayCommand(AddNewPerson, (canExecute) => true);
            PersonSelectedCommand = new RelayCommand<PersonListModel>(PersonSelected, (canExecute) => true);
            Messenger.Default.Register<PersonDetailModel>(this, NewPersonReceived, PersonDetailViewModel.AddPersonToken);
            Messenger.Default.Register<PersonDetailModel>(this, UpdatePersonReceived, PersonDetailViewModel.UpdatePersonToken);
            Messenger.Default.Register<Guid>(this, DeletePersonGuidReceived, PersonDetailViewModel.DeletePersonToken);
        }

        public ObservableCollection<PersonListModel> People { get; } = new ObservableCollection<PersonListModel>();

        public ICommand PersonDetailCommand { get; }
        public ICommand PersonSelectedCommand { get; }

        private void AddNewPerson(object x = null)
        {
            var newPersonWrapper = new PersonDetailModel
            {
                Id = Guid.NewGuid()
            };

            Messenger.Default.Send(newPersonWrapper, AddNewPersonToken);
        }

        private void NewPersonReceived(PersonDetailModel personDetailModel)
        {
            _personRepository.Create(personDetailModel);
            Load(_personRepository);
        }

        private void UpdatePersonReceived(PersonDetailModel personDetailModel)
        {
            _personRepository.Update(personDetailModel);
            Load(_personRepository);
        }

        private void DeletePersonGuidReceived(Guid id)
        {
            _personRepository.Delete(id);
            Load(_personRepository);
        }

        private void PersonSelected(PersonListModel personListModel)
        {
            var personDetailViewModel = _personRepository.GetById(personListModel.Id);
            var personSelectedDetailModel = new PersonDetailModel()
            {
                Id = personDetailViewModel.Id,
                Name = personDetailViewModel.Name,
                Surname = personDetailViewModel.Surname,
                Age = personDetailViewModel.Age,
                PictureUrl = personDetailViewModel.PictureUrl,
                ActedInMovies = personDetailViewModel.ActedInMovies,
                DirectedMovies = personDetailViewModel.DirectedMovies
            };

            Messenger.Default.Send(personSelectedDetailModel, PersonSelectedToken);
        }

        private void Load(IPeopleRepository personRepository)
        {
            People.Clear();
            _personRepository = personRepository;
            var people = _personRepository.GetAll();
            People.AddRange(people);
        }


        public static readonly Guid AddNewPersonToken = Guid.Parse("057B5DF5-4480-47EF-8A5A-ED0159C15A93");
        public static readonly Guid PersonSelectedToken = Guid.Parse("788FC5E8-41FB-4D82-9A35-827AE67A6D2A");
    }
}


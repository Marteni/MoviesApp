using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MoviesApp.APP.Command;
using MoviesApp.APP.Enums;
using MoviesApp.APP.Services;
using MoviesApp.App.ViewModels;
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
            Messenger.Default.Register<PersonListModel>(this, SelectedPersonReceived, MovieDetailViewModel.SelectedPersonToken);
        }

        private void SelectedPersonReceived(PersonListModel selectedPerson)
        {
            var person = People.FirstOrDefault(t => t.Id == selectedPerson.Id);
            PersonSelected(person);
            var value = (int) TabEnums.PeopleTab;
            Messenger.Default.Send(value, MainViewModel.ChangeTabToken);
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
            UpdatePeopleListViewWithNewItem(personDetailModel);
        }

        private void PersonSelected(PersonListModel personListModel)
        {
            var personDetailViewModel = _personRepository.GetById(personListModel.Id);
            Messenger.Default.Send(personDetailViewModel, PersonSelectedToken);
        }


        private void UpdatePersonReceived(PersonDetailModel personDetailModel)
        {
            _personRepository.Update(personDetailModel);
            UpdatePeopleListWithExistingItem(personDetailModel);
        }

        private void DeletePersonGuidReceived(Guid id)
        {
            _personRepository.Delete(id);
            People.Remove(People.First(t => t.Id == id));
        }

        private void UpdatePeopleListViewWithNewItem(PersonDetailModel personDetailModel)
        {
            var movieListModel = new PersonListModel()
            {
                Id = personDetailModel.Id,
                Name = personDetailModel.Name,
                Surname = personDetailModel.Surname
            };
            People.Add(movieListModel);
        }
        private void UpdatePeopleListWithExistingItem(PersonDetailModel personDetailModelUpdated)
        {
            var item = People.FirstOrDefault(a => a.Id == personDetailModelUpdated.Id);
            var index = People.IndexOf(item);

            if (index != -1)
            {
                People[index].Name = personDetailModelUpdated.Name;
                People[index].Surname = personDetailModelUpdated.Surname;
                CollectionViewSource.GetDefaultView(People).Refresh();
            }
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


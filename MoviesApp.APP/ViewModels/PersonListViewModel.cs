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
            
            Messenger.Default.Register<PersonListModel>(this, SelectedPersonReceived);
            Messenger.Default.Register<PersonNewFilledWrapper>(this, NewPersonReceived);
            Messenger.Default.Register<PersonEditWrapper>(this, UpdatePersonReceived);
            Messenger.Default.Register<PersonDeleteGuidWrapper>(this, DeletePersonGuidReceived);
        }

        public ICommand PersonDetailCommand { get; }
        public ICommand PersonSelectedCommand { get; }

        public ObservableCollection<PersonListModel> People { get; } = new ObservableCollection<PersonListModel>();


        private void SelectedPersonReceived(PersonListModel selectedPerson)
        {
            var person = People.FirstOrDefault(t => t.Id == selectedPerson.Id);
            PersonSelected(person);
            var value = (int) TabEnums.PeopleTab;
            Messenger.Default.Send(value);
        }
        
        private void AddNewPerson(object x = null)
        {
            var newPersonWrapper = new PersonNewWrapper
            {
                Id = Guid.NewGuid()
            };

            Messenger.Default.Send(newPersonWrapper);
        }

        private void NewPersonReceived(PersonNewFilledWrapper personNewWrapper)
        {
            var personDetailModel = WrapperMappers.ToPersonDetailModel(personNewWrapper);
            _personRepository.Create(personDetailModel);
            UpdatePeopleListViewWithNewItem(personDetailModel);
        }

        private void PersonSelected(PersonListModel personListModel)
        {
            var personDetailViewModel = _personRepository.GetById(personListModel.Id);
            Messenger.Default.Send(WrapperMappers.PersonDetailToPersonSelectedWrapper(personDetailViewModel));
        }

        private void UpdatePersonReceived(PersonEditWrapper personEditWrapper)
        {
            var personDetailModel = WrapperMappers.ToPersonDetailModel(personEditWrapper);
            _personRepository.Update(personDetailModel);
            UpdatePeopleListWithExistingItem(personDetailModel);
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
        
        private void DeletePersonGuidReceived(PersonDeleteGuidWrapper idWrapper)
        {
            var id = WrapperMappers.PersonDeleteGuidWrapperToGuid(idWrapper);
            _personRepository.Delete(id);
            People.Remove(People.First(t => t.Id == id));
        }

        private void Load(IPeopleRepository personRepository)
        {
            People.Clear();
            _personRepository = personRepository;
            var people = _personRepository.GetAll().OrderBy(p => p.Surname).ThenBy(p => p.Name);
            People.AddRange(people);
        }

    }
}


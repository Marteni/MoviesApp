using MoviesApp.BL.Models;
using System;
using System.Collections.Generic;

namespace MoviesApp.BL.Repositories
{
    public interface IPeopleRepository
    {
        IList<PersonListModel> GetAll();
        PersonDetailModel GetById(Guid id);
        PersonListModel GetByIdListModel(Guid id);
        PersonDetailModel Create(PersonDetailModel model);
        void Update(PersonDetailModel model);
        void Delete(Guid id);
    }
}

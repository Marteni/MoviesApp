using MoviesApp.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesApp.BL.Repositories
{
    public interface IPeopleRepository
    {
        IList<PersonListModel> GetAll();
        PersonDetailModel GetById(Guid id);
        PersonDetailModel Create(PersonDetailModel model);
        void Update(PersonDetailModel model);
        void Delete(Guid id);
    }
}

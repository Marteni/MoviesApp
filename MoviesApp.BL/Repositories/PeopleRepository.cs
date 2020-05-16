using System;
using System.Collections.Generic;
using System.Text;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;
using System.Linq;

namespace MoviesApp.BL.Repositories
{
    public class PeopleRepository : IPeopleRepository
    {

        private readonly IDbContextSqlFactory _dbContextSqlFactory;

        public PeopleRepository(IDbContextSqlFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<PersonListModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.People
                    .Select(e => PersonMapper.MapPersonEntityToListModel(e))
                    .ToList();
            }
        }

        public PersonDetailModel GetById(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.People.First(t => t.Id == id);
                return PersonMapper.MapPersonEntityToDetailModel(entity);
            }
        }

        public PersonListModel GetByIdListModel(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.People.FirstOrDefault(t => t.Id == id);
                if (entity == null) return null;
                return PersonMapper.MapPersonEntityToListModel(entity);
            }
        }

        public PersonDetailModel Create(PersonDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = PersonMapper.MapPersonDetailModelToEntity(model);
                dbContext.People.Add(entity);
                dbContext.SaveChanges();
                return PersonMapper.MapPersonEntityToDetailModel(entity);
            }
        }

        public void Update(PersonDetailModel model)
        {
            using (var dbContext =  _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = PersonMapper.MapPersonDetailModelToEntity(model);
                dbContext.People.Update(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.People.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}

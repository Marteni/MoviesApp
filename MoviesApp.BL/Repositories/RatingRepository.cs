using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesApp.BL.Mappers;
using MoviesApp.BL.Models;
using MoviesApp.DAL.Factories;

namespace MoviesApp.BL.Repositories
{
    public class RatingRepository : IRatingRepository
    {

        private readonly IDbContextSqlFactory _dbContextSqlFactory;

        public RatingRepository(IDbContextSqlFactory dbContextSqlFactory)
        {
            this._dbContextSqlFactory = dbContextSqlFactory;
        }

        public IList<RatingDetailModel> GetAll()
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                return dbContext.Ratings
                    .Select(e => RatingMapper.MapRatingEntityToDetailModel(e))
                    .ToList();
            }
        }

        public RatingDetailModel GetById(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                //SELECT * FROM Ingredient WHERE Id = id;
                var entity = dbContext.Ratings.First(t => t.Id == id);
                return RatingMapper.MapRatingEntityToDetailModel(entity);
            }
        }

        public RatingDetailModel Create(RatingDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = RatingMapper.MapRatingDetailModelToEntity(model);
                dbContext.Ratings.Add(entity);
                dbContext.SaveChanges();
                return RatingMapper.MapRatingEntityToDetailModel(entity);
            }
        }

        public void Update(RatingDetailModel model)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = RatingMapper.MapRatingDetailModelToEntity(model);
                dbContext.Ratings.Update(entity);
                dbContext.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var dbContext = _dbContextSqlFactory.CreateAppDbContext())
            {
                var entity = dbContext.Ratings.First(t => t.Id == id);
                dbContext.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}

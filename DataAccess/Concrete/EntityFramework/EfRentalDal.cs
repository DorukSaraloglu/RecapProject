using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : IRentalDal
    {
        public List<Rental> GetAll(Expression<Func<Rental, bool>> filter = null)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return filter == null ? context.Set<Rental>().ToList() : context.Set<Rental>().Where(filter).ToList();
            }
        }

        public Rental Get(Expression<Func<Rental, bool>> filter)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return context.Set<Rental>().SingleOrDefault(filter);
            }
        }

        public void Add(Rental entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Rental entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Rental entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}

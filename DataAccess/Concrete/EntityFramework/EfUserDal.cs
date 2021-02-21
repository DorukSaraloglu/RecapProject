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
    public class EfUserDal : IUserDal
    {
        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return filter == null ? context.Set<User>().ToList() : context.Set<User>().Where(filter).ToList();
            }
        }

        public User Get(Expression<Func<User, bool>> filter)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return context.Set<User>().SingleOrDefault(filter);
            }
        }

        public void Add(User entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(User entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(User entity)
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

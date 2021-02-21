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
    public class EfCustomerDal : ICustomerDal
    {
        public List<Customer> GetAll(Expression<Func<Customer, bool>> filter = null)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return filter == null ? context.Set<Customer>().ToList() : context.Set<Customer>().Where(filter).ToList();
            }
        }

        public Customer Get(Expression<Func<Customer, bool>> filter)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return context.Set<Customer>().SingleOrDefault(filter);
            }
        }

        public void Add(Customer entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Customer entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Customer entity)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return filter == null ? context.Set<Car>().ToList() : context.Set<Car>().Where(filter).ToList();
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                return context.Set<Car>().SingleOrDefault(filter);
            }
        }

        public void Add(Car entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Car entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Car entity)
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public List<CarDetailDto> GetCarDetails()
        {
            using (RecapProjectDBContext context = new RecapProjectDBContext())
            {
                var result =
                    from car in context.Cars
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    select new CarDetailDto
                    {
                        CarId = car.Id,
                        BrandName = brand.Name,
                        ColorName = color.Name,
                        ModelYear = car.ModelYear,
                        DailyPrice = car.DailyPrice
                    };
                return result.ToList();
            }
        }
    }
}

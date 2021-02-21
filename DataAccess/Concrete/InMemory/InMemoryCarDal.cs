using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal :ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal(Car car)
        {
            _cars = new List<Car>
            {
                new Car{Id = 1, BrandId = 1, ColorId = 1, ModelYear = "1998", DailyPrice = 100, Description = ""},
                new Car{Id = 2, BrandId = 2, ColorId = 2, ModelYear = "2008", DailyPrice = 200, Description = ""},
                new Car{Id = 2, BrandId = 2, ColorId = 2, ModelYear = "2018", DailyPrice = 300, Description = ""}
            };
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.Id == id).ToList();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            Car carUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carUpdate.BrandId = car.BrandId;
            carUpdate.ColorId = car.ColorId;
            carUpdate.ModelYear = car.ModelYear;
            carUpdate.DailyPrice = car.DailyPrice;
            carUpdate.Description = car.Description;
        }

        public void Delete(Car car)
        {
            _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(car);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}

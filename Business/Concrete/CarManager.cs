using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private IBrandDal _brandDal;

        public CarManager(ICarDal carDal, IBrandDal brandDal)
        {
            _carDal = carDal;
            _brandDal = brandDal;
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenenceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);
        }

        public IDataResult<List<Car>> GetByPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max), Messages.Listed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.Listed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.Listed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenenceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.Listed);
        }
        
        public IResult Add(Car car)
        {
            //string brandName = _brandDal.Get(c => c.Id == car.BrandId).Name;
            //if (brandName.Length < 2 && car.DailyPrice < 0)
            //{
            //    _carDal.Add(car);
            //    return new SuccessResult(Messages.NotAdded);
            //}

            //ValidationTool.Validate(new CarValidator(), car);

            //Console.WriteLine("Marka adı minimum 2 karakter olmalıdır ve günlük ücret 0 tlden fazla olmalıdır!");
            _carDal.Add(car);
            return new ErrorResult(Messages.Added);
        }

        public IResult Update(Car car)
        {
            string brandName = _brandDal.Get(c => c.Id == car.BrandId).Name;
            if (brandName.Length > 2 && car.DailyPrice > 0)
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.Updated);
            }
            else
            {
                //Console.WriteLine("Marka adı minimum 2 karakter olmalıdır ve günlük ücret 0 tlden fazla olmalıdır!");
                return new ErrorResult(Messages.NotUpdated);
            }
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }
    }
}

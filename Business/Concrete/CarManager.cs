using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using ValidationException = FluentValidation.ValidationException;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private IBrandDal _brandDal;
        private IColorDal _colorDal;


        public CarManager(ICarDal carDal, IBrandDal brandDal, IColorDal colorDal)
        {
            _carDal = carDal;
            _brandDal = brandDal;
            _colorDal = colorDal;
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAll()
        {
            //if (DateTime.Now.Hour == 22)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.MaintenenceTime);
            //}
            //return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);

            var carsDataResult = new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);

            var cars = carsDataResult.Data.Select(c => new CarDetailDto
            {
                DailyPrice = c.DailyPrice,
                CarId = c.Id,
                ModelYear = c.ModelYear,
                BrandName = _brandDal.Get(b => b.Id == c.BrandId).Name,
                ColorName = _colorDal.Get(o => o.Id == c.ColorId).Name,
                Description = c.Description
            }).ToList();

            return new SuccessDataResult<List<CarDetailDto>>(cars);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetByPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max), Messages.Listed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.Listed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.Listed);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenenceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.Listed);
        }

        [ValidationAspect(typeof(CarValidator))]
        //[SecuredOperation("car.add,admin")]           //ÇALIŞIRKEN HATA VERİYOR
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }
    }
}

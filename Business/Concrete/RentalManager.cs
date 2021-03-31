using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarDal _carDal;
        private IBrandDal _brandDal;
        private ICustomerDal _customerDal;

        public RentalManager(IRentalDal rentalDal, IBrandDal brandDal, ICustomerDal customerDal, ICarDal carDal)
        {
            _rentalDal = rentalDal;
            _brandDal = brandDal;
            _customerDal = customerDal;
            _carDal = carDal;
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Listed);
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetRentalDetail()
        {
            var data = new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Listed);
            
            var detail = data.Data.Select(d => new RentalDetailDto
            {
                Id = d.Id,
                BrandName = (from car in  _carDal.GetAll()
                    join brand in _brandDal.GetAll() on car.BrandId equals brand.Id where car.Id ==d.CarId
                            select brand.Name).FirstOrDefault(),
                CompanyName = _customerDal.Get(c => c.Id == d.CustomerId).CompanyName,
                RentDate = d.RentDate,
                ReturnDate = d.ReturnDate
            }).ToList();

            return new SuccessDataResult<List<RentalDetailDto>>(detail);
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.Listed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [SecuredOperation("rental.add,admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            if (rental.ReturnDate > DateTime.Now)
            {
                return new ErrorResult(Messages.NotAdded);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdd);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            if (rental.ReturnDate > DateTime.Now)
            {
                return new ErrorResult(Messages.NotRentalUpdate);
            }
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdate);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDelete);
        }
    }
}

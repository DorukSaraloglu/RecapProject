using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SixLabors.ImageSharp;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        protected readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CarImageManager(ICarImageDal carImageDal, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _carImageDal = carImageDal;
            _hostingEnvironment = hostingEnvironment;
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.Listed);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id), Messages.Listed);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(/*CarImage carImage,*/ IFormFile formFile)
        {
            string folderName = "CarImages";
            string webRootPath = _hostingEnvironment.ContentRootPath;
            string newPath = Path.Combine(webRootPath, folderName);


            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
                newPath = Path.Combine(newPath, Guid.NewGuid().ToString() + "." + formFile.FileName.Split('.').LastOrDefault());

                var sourcepath = Path.GetTempFileName();
                if (formFile.Length > 0)
                    using (var stream = new FileStream(sourcepath, FileMode.Create))
                        formFile.CopyTo(stream);
                File.Move(sourcepath, newPath);
            }
            else
            {
                newPath = Path.Combine(newPath, Guid.NewGuid().ToString() + "." +formFile.FileName.Split('.').LastOrDefault());

                var sourcepath = Path.GetTempFileName();
                if (formFile.Length > 0)
                    using (var stream = new FileStream(sourcepath, FileMode.Create))
                        formFile.CopyTo(stream);
                File.Move(sourcepath, newPath);
            }

                return new SuccessResult(Messages.Added);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.Updated);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.Deleted);
        }
    }
}

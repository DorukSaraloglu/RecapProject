using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int id);
        IResult Add(/*CarImage carImage, */IFormFile formFile);
        IResult Update(CarImage carImage);
        IResult Delete(CarImage carImage);
    }
}

using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.CrossCuttingConcerns.Validation;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetAll()
        {
            var result = _carDal.GetAll();
            if (result == null) return new ErrorDataResult<List<Car>>(Messages.DataNotFound);
            return new SuccessDataResult<List<Car>>(result, Messages.CarListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            if (result == null)
                return new ErrorDataResult<Car>(Messages.DataNotFound);
            return new SuccessDataResult<Car>(result);

        }

        public IDataResult<List<CarDetailDto>> GetCarDetailDto()
        {
            var result = _carDal.GetCarDetailDto();
            
            if (DateTime.Now.Hour == 1)
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);

            if (result == null)
                return new ErrorDataResult<List<CarDetailDto>>(Messages.DataNotFound);

            return new SuccessDataResult<List<CarDetailDto>>(result, Messages.CarDetailListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            var result = _carDal.GetAll(c => c.BrandId == id);
            if (result == null)
                return new ErrorDataResult<List<Car>>(Messages.DataNotFound);
            return new SuccessDataResult<List<Car>>(result);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            var result = _carDal.GetAll(c => c.ColorId == id);
            if (result == null)
                return new ErrorDataResult<List<Car>>(Messages.DataNotFound);
            return new SuccessDataResult<List<Car>>(result);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantSave);
            _carDal.Add(entity);
            return new SuccessResult(Messages.CarAdded);
        }
        
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car entity)
        {
            if (entity == null)
                return new ErrorDataResult<List<Car>>(Messages.DataCantUpdate);
            _carDal.Update(entity);
            return new SuccessResult(Messages.CarUpdated);
        }
        
        public IResult Delete(Car entity)
        {
            if (entity == null)
                return new ErrorDataResult<List<Car>>(Messages.DataCantDelete);
            _carDal.Delete(entity);
            return new SuccessResult(Messages.CarDeleted);
        }
    }
}

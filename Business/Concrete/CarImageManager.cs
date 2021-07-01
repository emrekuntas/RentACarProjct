using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImages entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataNotFound);
            
            var result = BusinessRules.Run(CheckCarsImageCount(entity.CarId));
            if (result != null) return result;

            entity.CreateDate = DateTime.Now;
            _carImageDal.Add(entity);
            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult CheckCarsImageCount(int cardId)
        {
            var result = _carImageDal.GetAll(i=>i.CarId==cardId).Count;
            if (result>5)
            {
                return new ErrorResult(Messages.ImageCountExceeded);
            }
            return new SuccessResult();
        }

        public IResult Delete(CarImages entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataNotFound);
            _carImageDal.Delete(entity);
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImages>> GetAll()
        {
            var result = _carImageDal.GetAll();
            if (result == null) return new ErrorDataResult<List<CarImages>>(Messages.DataNotFound);
            return new SuccessDataResult<List<CarImages>>(result, Messages.ImagesListed);
        }

        //arabaya ait resim yoksa şirket logosu gösterilecek
        public IDataResult<List<CarImages>> GetAllByCarId(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId);
            if (result == null) 
                return new ErrorDataResult<List<CarImages>>(Messages.DataNotFound);

            return new SuccessDataResult<List<CarImages>>(result, Messages.ImagesListed);
        }

        public IDataResult<CarImages> GetById(int id)
        {
            var result = _carImageDal.Get(c => c.Id == id);
            if (result == null) return new ErrorDataResult<CarImages>(Messages.DataNotFound);
            return new SuccessDataResult<CarImages>(result);
        }

        public IResult Update(CarImages entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataNotFound);
            _carImageDal.Update(entity);
            return new SuccessResult(Messages.ImageUpdated);
        }
    }
}

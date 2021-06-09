using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public IDataResult<List<Brand>> GetAll()
        {
            var result = _brandDal.GetAll();
            if (result == null) return new ErrorDataResult<List<Brand>>(Messages.DataNotFound);
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Brand>>(result, Messages.BrandListed);
        }
        public IDataResult<Brand> GetById(int id)
        {
            var result = _brandDal.Get(b => b.BrandId == id);
            if (result == null)
                return new ErrorDataResult<Brand>(Messages.DataNotFound);
            return new SuccessDataResult<Brand>(result);
        }
        
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantSave);
            if (_brandDal.GetAll().Any(b => string.Equals(b.BrandName, entity.BrandName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new ErrorResult(Messages.BrandAlreadyExist);
            }
            _brandDal.Add(entity);
            return new SuccessResult(Messages.BrandAdded);
        }
        [ValidationAspect(typeof(BrandValidator))]
       
        
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantUpdate);
            _brandDal.Update(entity);
            return new SuccessResult(Messages.BrandUpdated);
        }
        public IResult Delete(Brand entity)
        {
            if (entity == null)
                return new ErrorResult(Messages.DataCantDelete);
            _brandDal.Delete(entity);
            return new SuccessResult(Messages.BrandDeleted);
        }
    }
}

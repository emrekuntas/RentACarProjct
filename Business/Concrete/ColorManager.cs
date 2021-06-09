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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            var result = _colorDal.GetAll();
            if (result == null) return new ErrorDataResult<List<Color>>(Messages.DataNotFound);
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Color>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Color>>(result, Messages.ColorListed);
        }
        public IDataResult<Color> GetById(int id)
        {
            var result = _colorDal.Get(c => c.ColorId == id);
            if (result == null)
                return new ErrorDataResult<Color>(Messages.DataNotFound);
            return new SuccessDataResult<Color>(result);
        }
        
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantSave);
            if (_colorDal.GetAll().Any(c => string.Equals(entity.ColorName, c.ColorName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new ErrorResult(Messages.ColorAlreadyExist);
            }
            _colorDal.Add(entity);
            return new SuccessResult(Messages.ColorAdded);

        }
        
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantUpdate);
            _colorDal.Update(entity);
            return new SuccessResult(Messages.ColorUpdated);
        }
        public IResult Delete(Color entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantDelete);
            _colorDal.Delete(entity);
            return new SuccessResult(Messages.ColorDeleted);
        }
    }
}

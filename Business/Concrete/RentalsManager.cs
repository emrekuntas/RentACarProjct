using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalsManager : IRentalsService
    {
        IRentalsDal _rentalsDal;

        public RentalsManager(IRentalsDal rentalsDal)
        {
            _rentalsDal = rentalsDal;
        }
        public IDataResult<List<Rentals>> GetAll()
        {
            var result = _rentalsDal.GetAll();
            if (result == null)
                return new ErrorDataResult<List<Rentals>>(Messages.DataNotFound);
            return new SuccessDataResult<List<Rentals>>(result, Messages.RentalsListed);
        }

        public IDataResult<Rentals> GetById(int id)
        {
            var result = _rentalsDal.Get(r => r.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Rentals>(result);
            }
            return new ErrorDataResult<Rentals>(Messages.DataNotFound);
        }

        public IResult Add(Rentals entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantSave);
            var isRental = _rentalsDal.Get(r => r.CarId == entity.CarId);
            if (isRental is { ReturnDate: null })
            {
                return new ErrorResult(Messages.CarNotSuitable);
            }
            _rentalsDal.Add(entity);
            return new SuccessResult(Messages.RentalsAdded);
        }

        public IResult Delete(Rentals entity)
        {
            if (entity == null) 
                return new ErrorResult(Messages.DataCantDelete);
            _rentalsDal.Delete(entity);
            return new SuccessResult(Messages.RentalsDeleted);
        }

        public IResult Update(Rentals entity)
        {
            if (entity == null)
                return new ErrorResult(Messages.DataCantUpdate);
            _rentalsDal.Update(entity);
            return new SuccessResult(Messages.RentalsUpdated);
        }
    }
}

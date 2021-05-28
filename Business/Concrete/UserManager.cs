using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = _userDal.GetAll();
            if (result == null) return new ErrorDataResult<List<User>>(Messages.DataNotFound);
            return new SuccessDataResult<List<User>>(result, Messages.UserListed);
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if (result == null)
                return new ErrorDataResult<User>(Messages.DataNotFound);
            return new SuccessDataResult<User>(result);

        }

        public IResult Add(User entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantSave);
            if (_userDal.GetAll().Any(u=>string.Equals(u.Email,entity.Email,StringComparison.CurrentCultureIgnoreCase)))
            {
                return new ErrorResult(Messages.EmailAlreadyUsed);
            } 
            _userDal.Add(entity);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(User entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantDelete);
            _userDal.Delete(entity);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IResult Update(User entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantUpdate);
            _userDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

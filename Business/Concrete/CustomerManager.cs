using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            if (result == null)
                return new ErrorDataResult<List<Customer>>(Messages.DataNotFound);

            return new SuccessDataResult<List<Customer>>(result, Messages.CustomerListed);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Customer>(result);
            }
            return new ErrorDataResult<Customer>(Messages.DataNotFound);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer entity)
        {
            if (entity == null) return new ErrorResult(Messages.DataCantSave);
            if (_customerDal.GetAll().Any(c =>
                string.Equals(c.CompanyName, entity.CompanyName, StringComparison.CurrentCultureIgnoreCase)))
                return new ErrorResult(Messages.CompanyNameAlreadyExist);
            _customerDal.Add(entity);
            return new SuccessResult(Messages.CustomerAdded);
        }

        public IResult Delete(Customer entity)
        {
            if (entity == null)
                return new ErrorResult(Messages.DataCantDelete);
            _customerDal.Delete(entity);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer entity)
        {
            if (entity == null)
                return new ErrorResult(Messages.DataCantUpdate);
            _customerDal.Update(entity);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}

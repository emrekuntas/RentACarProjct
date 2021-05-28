using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car> 
            {
            new Car{Id=1,BrandId=1,ColorId=1,DailyPrice=100,Description="Hundai",ModelYear=1994},
            new Car{Id=2,BrandId=1,ColorId=1,DailyPrice=50,Description="Toyota",ModelYear=1996},
            new Car{Id=3,BrandId=2,ColorId=2,DailyPrice=40,Description="BMW",ModelYear=2001},
            new Car{Id=4,BrandId=3,ColorId=3,DailyPrice=15,Description="Audi",ModelYear=2003},
            new Car{Id=5,BrandId=3,ColorId=4,DailyPrice=70,Description="Mustang",ModelYear=1995},
            new Car{Id=6,BrandId=4,ColorId=5,DailyPrice=5,Description="Mercedes",ModelYear=1978},
            };
        }


        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=>c.Id==car.Id);
            _cars.Remove(carToDelete);

        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars.ToList();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(Car car)
        {
            return _cars.Where(c => c.Id == car.Id).SingleOrDefault();
        }

        public List<CarDetailDto> GetCarDetailDto()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.Description = car.Description;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.BrandId = car.BrandId;
        }
    }
}

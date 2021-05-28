using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetailDto()
        {
            using RentACarContext context = new RentACarContext();
            var list = from c in context.Cars
                       join b in context.Brands on c.BrandId equals b.BrandId
                       join cl in context.Colors on c.ColorId equals cl.ColorId
                       orderby c.Id
                       select new CarDetailDto
                       {
                           BrandName = b.BrandName,
                           CarId = c.Id,
                           CarName = c.CarName,
                           ColorName = cl.ColorName,
                           DailyPrice = c.DailyPrice
                       };

            return list.ToList();
        }
    }
}

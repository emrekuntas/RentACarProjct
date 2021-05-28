using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    public class Program
    {

        static void Main(string[] args)
        {
            RentalsTest();
            //CustomerTest();
            //UserTest();
            //CarTest();
            //BrandTest();
            //ColorTest();
        }

        public static void UserTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());

            //userManager.Add(new User { FirstName = "Emre", LastName = "Küntaş", Email = "kuntas@gmail.com", Password = "test1" });
            //userManager.Add(new User { FirstName = "Deniz", LastName = "Küntaş", Email = "deniz@gmail.com", Password = "test2" });
            //userManager.Add(new User { FirstName = "Diyar", LastName = "Küntaş", Email = "deniz@gmail.com", Password = "test3" });

            foreach (var item in userManager.GetAll().Data)
            {
                Console.WriteLine(item.FirstName+" "+item.LastName);
            }

        }

        public static void CustomerTest()
        {
            var customerManager = new CustomerManager(new EfCustomerDal());

            customerManager.Add(new Customer {UserId = 1});
            customerManager.Add(new Customer { UserId = 2,CompanyName ="Test"});
            customerManager.Add(new Customer { UserId = 3,CompanyName = "test2"});

            foreach (var item in customerManager.GetAll().Data)
            {
                Console.WriteLine(item.UserId + " " + item.CompanyName);
            }

        }

        public static void RentalsTest()
        {
            var rentalsManager = new RentalsManager(new EfRentalsDal());

            //var test1=rentalsManager.Add(new Rentals {CarId = 3, RentDate = DateTime.Now, CustomerId = 1});
            var test2= rentalsManager.Add(new Rentals {CarId = 2, RentDate = DateTime.Now, CustomerId = 2});

            Console.WriteLine(test2.Message);



        }

        private static void ColorTest()
        {
            var colorManager = new ColorManager(new EfColorDal());
            Console.WriteLine(colorManager.Add(new Color { ColorName = "Mavi" }));
            Console.WriteLine(colorManager.Add(new Color { ColorName = "Siyah" }));
            foreach (var item in colorManager.GetAll().Data)
            {
                Console.WriteLine(item.ColorId + " " + item.ColorName);
            }

        }

        private static void BrandTest()
        {
            var brandManager = new BrandManager(new EfBrandDal());

            Console.WriteLine(brandManager.Add(new Brand { BrandName = "Hyundai" }));
            foreach (var item in brandManager.GetAll().Data)
            {
                Console.WriteLine(item.BrandId + " " + item.BrandName);
            }
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());

            //var result = carManager.Add(new Car { DailyPrice = 12, BrandId = 2, CarName = "Mercedenz Benz", ColorId = 3, Description = "Test", ModelYear = 1995 });
            //Console.WriteLine(result);

            var result = carManager.GetCarDetailDto();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.CarId + "/" + item.CarName + "/" + item.BrandName + "/"
                        + item.ColorName + "/" + item.DailyPrice);
                }
            }
            else 
            {
                Console.WriteLine(result.Message);
            }


        }
    }
}

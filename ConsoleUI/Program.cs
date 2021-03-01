using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entity.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarCrud();
            //Console.WriteLine("--------------------------");
            //BrandCrud();
            //Console.WriteLine("--------------------------");
            //ColorCrud();
            //Console.WriteLine("--------------------------");
            //UserCrud();
            //Console.WriteLine("--------------------------");
            //CustomerCrud();
            //Console.WriteLine("--------------------------");
            RentalCrud();
        }

        private static void RentalCrud()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());

            Rental rent1 = new Rental
            {
                CarId = 1,
                CustomerId = 1,
                RentDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(1)
            };
            var result = rentalManager.Add(rent1);

            foreach (var rental in rentalManager.GetAll().Data)
            {
                Console.WriteLine("{0} - {1} - {2} - {3} - {4}", rental.Id, rental.CarId, rental.CustomerId, rental.RentDate, rental.ReturnDate);
            }

            Console.WriteLine(result.Message);
        }

        private static void UserCrud()
        {
            User user1 = new User
            {
                FirstName = "A",
                LastName = "B",
                Email = "a@hotmail.com",
                Password = "1"
            };
        }

        private static void CustomerCrud()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

            Customer customer1 = new Customer
            {
                UserId = 1,
                CompanyName = "Xiaomi"
            };

            customerManager.Add(customer1);

            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine("{0} - {1} - {2}", customer.Id, customer.UserId, customer.CompanyName);
            }
        }

        private static void CarCrud()
        {
            //Console.WriteLine("BrandId : ");
            //int _brandId = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("ColorId : ");
            //int _colorId = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("DailyPrice : ");
            //decimal _dailyPrice = Convert.ToDecimal(Console.ReadLine());
            //Console.WriteLine("ModelYear : ");
            //string _modelYear = Console.ReadLine();

            //CarManager carManager = new CarManager(new EfCarDal(), new EfBrandDal());

            Car car1 = new Car
            {
                BrandId = 1,
                ColorId = 1,
                DailyPrice = 120,
                ModelYear = "1985"
            };
            //carManager.Add(car1);

            Car car2 = new Car
            {
                BrandId = 2,
                ColorId = 2,
                DailyPrice = 250,
                ModelYear = "2000"
            };
            //carManager.Add(car2);

            Car car3 = new Car
            {
                BrandId = 3,
                ColorId = 4,
                DailyPrice = 350,
                ModelYear = "2007"
            };
            //carManager.Add(car3);

            //carManager.Update(car2);
            //Console.WriteLine("Updated");
            //carManager.Delete(car3);
            //Console.WriteLine("Deleted");

            //foreach (var car in carManager.GetAll().Data)
            //{
            //    Console.WriteLine("Id: {0} - BrandId: {1} - ColorId: {2} - ModelYear: {3} - DailyPrice: {4} - Description: {5}",
            //        car.Id, car.BrandId, car.ColorId, car.ModelYear, car.DailyPrice, car.Description);
            //}

            //var result = carManager.GetById(1).Data;
        }

        private static void BrandCrud()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            Brand brand1 = new Brand()
            {
                Name = "Ferrari"
            };
            //brandManager.Add(brand1);

            Brand brand2 = new Brand()
            {
                Name = "Lamborghini"
            };
            //brandManager.Add(brand2);

            Brand brand3 = new Brand()
            {
                Name = "Porsche"
            };
            //brandManager.Add(brand3);

            brandManager.Update(brand2);
            Console.WriteLine("Updated");
            brandManager.Delete(brand3);
            Console.WriteLine("Deleted");

            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine("Id: {0} - Name: {1}", brand.Id, brand.Name);
            }
        }

        private static void ColorCrud()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());

            Color color1 = new Color
            {
                Name = "Kahverengi"
            };
            //colorManager.Add(color1);

            Color color2 = new Color
            {
                Name = "Mor"
            };
            //colorManager.Add(color2);

            Color color3 = new Color
            {
                Name = "Turuncu"
            };
            //colorManager.Add(color3);

            colorManager.Update(color2);
            Console.WriteLine("Updated");
            colorManager.Delete(color3);
            Console.WriteLine("Deleted");

            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine("Id: {0} - Name: {1}", color.Id, color.Name);
            }
        }
    }
}

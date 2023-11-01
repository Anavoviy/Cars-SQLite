using Cars.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Services
{
    public class DataBase
    {
        private int AuthorizationUserId = 0;

        public DataBase()
        {
            
        }

        //Users
        public async Task<User> GetUserAsync(int id)
        {
            User user = EFSingleTon.instance.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return null;
            return user;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            if (user == null)
                return false;

            EFSingleTon.instance.Users.Add(user);
            EFSingleTon.instance.SaveChanges();
            return true;
        }


        //Cars
        public async Task<List<Car>> GetCarsAsync()
        {
            if(AuthorizationUserId == 0)
                return EFSingleTon.instance.Cars.ToList();
            else
            {
                List<Car> cars = EFSingleTon.instance.Cars.Where(c => c.UserId == AuthorizationUserId).ToList();
                return cars;
            }
        }
        public async Task<Car> GetCarAsync(int id)
        {
            Car car = EFSingleTon.instance.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return null;
            return car;
        }

        public async Task<bool> AddCarAsync(Car car)
        {
            if(car == null)
                return false;

            string engineView = "";
            string bodyTypeView = "";

            Engine engine = DB.instance.GetEngineAsync(car.IdEngine).Result;
            engineView += ("Двигатель: " + engine.Model + "; " + engine.CylinderArrangement + " " + engine.CylinderCapacity);
            
            BodyType bodyType = DB.instance.GetBodyTypeAsync(car.IdBodyType).Result;
            bodyTypeView += ("Кузов: " + bodyType.Title);
            
            if(AuthorizationUserId != 0)
                car.UserId = AuthorizationUserId;

            car.EngineView = engineView;
            car.BodyTypeView = bodyTypeView;

            EFSingleTon.instance.Cars.Add(car);
            EFSingleTon.instance.SaveChanges();
            return true;
        }
        public async Task<bool> UpdateCarAsync(Car car)
        {
            if (car == null || EFSingleTon.instance.Cars.FirstOrDefault(c => c.Id == car.Id) == null)
                return false;

            Car oldCar = EFSingleTon.instance.Cars.FirstOrDefault(c => c.Id == car.Id);
            oldCar.Model = car.Model;
            oldCar.Description = car.Description;
            oldCar.IdEngine = car.IdEngine;
            oldCar.IdBodyType = car.IdBodyType;

            Engine engine = DB.instance.GetEngineAsync(car.IdEngine).Result;
            BodyType bodyType = DB.instance.GetBodyTypeAsync(car.IdBodyType).Result;

            oldCar.EngineView = ("Двигатель: " + engine.Model + "; " + engine.CylinderArrangement + " " + engine.CylinderCapacity);
            oldCar.BodyTypeView = ("Кузов: " + bodyType.Title);

            EFSingleTon.instance.Cars.Update(oldCar);
            EFSingleTon.instance.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteCarAsync(Car car)
        {
            if(car == null || EFSingleTon.instance.Cars.FirstOrDefault(c => c.Id == car.Id) == null)
                return false;

            EFSingleTon.instance.Cars.Remove(car);
            EFSingleTon.instance.SaveChanges();
            return true;
        }

        //Engines
        public async Task<List<Engine>> GetEnginesAsync()
        {
            if (AuthorizationUserId == 0)
                return EFSingleTon.instance.Engines.ToList();
            else
            {
                return EFSingleTon.instance.Engines.Where(c => c.UserId == AuthorizationUserId).ToList();
            }
        }
        public async Task<Engine> GetEngineAsync(int id)
        {
            Engine engine = EFSingleTon.instance.Engines.FirstOrDefault(e => e.Id == id);
            if (engine == null)
                return null;
            return engine;
        }

        public async Task<bool> AddEngineAsync(Engine engine)
        {
            if (engine == null)
                return false;
            
            if (AuthorizationUserId != 0)
                engine.UserId = AuthorizationUserId;

            EFSingleTon.instance.Engines.Add(engine);
            EFSingleTon.instance.SaveChanges();
            return true;
        }
        public async Task<bool> UpdateEngineAsync(Engine engine)
        {
            if (engine == null || EFSingleTon.instance.Engines.FirstOrDefault(e => e.Id == engine.Id) == null)
                return false;

            Engine oldEngine = EFSingleTon.instance.Engines.FirstOrDefault(e => e.Id == engine.Id);
            oldEngine.Model = engine.Model;
            oldEngine.CylinderCapacity = engine.CylinderCapacity;
            oldEngine.CylinderArrangement = engine.CylinderArrangement;
            oldEngine.HorsePower = engine.HorsePower;

            EFSingleTon.instance.Engines.Update(oldEngine);
            EFSingleTon.instance.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteEngineAsync(Engine engine)
        {
            if (engine == null || EFSingleTon.instance.Engines.FirstOrDefault(e => e.Id == engine.Id) == null)
                return false;

            List<Car> cars = EFSingleTon.instance.Cars.Where(c => c.IdEngine == engine.Id).ToList();

            EFSingleTon.instance.Cars.RemoveRange(cars);
            EFSingleTon.instance.Engines.Remove(engine);
            EFSingleTon.instance.SaveChanges();
            return true;
        }

        //BodyTypes
        public async Task<List<BodyType>> GetBodyTypesAsync()
        {
            if (AuthorizationUserId == 0)
                return EFSingleTon.instance.BodyTypes.ToList();
            else
            {
                return EFSingleTon.instance.BodyTypes.Where(c => c.UserId == AuthorizationUserId).ToList();
            }
        }
        public async Task<BodyType> GetBodyTypeAsync(int id)
        {
            BodyType bodyType = EFSingleTon.instance.BodyTypes.FirstOrDefault(b => b.Id == id);
            if (bodyType == null)
                return null;

            return bodyType;
        }

        public async Task<bool> AddBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType == null)
                return false;


            if (AuthorizationUserId != 0)
                bodyType.UserId = AuthorizationUserId;

            EFSingleTon.instance.BodyTypes.Add(bodyType);
            EFSingleTon.instance.SaveChanges();
            return true;
        }
        public async Task<bool> UpdateBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType == null || EFSingleTon.instance.BodyTypes.FirstOrDefault(b => b.Id == bodyType.Id) == null)
                return false;

            BodyType oldBodyType = EFSingleTon.instance.BodyTypes.FirstOrDefault(b => b.Id == bodyType.Id);
            oldBodyType.Title = bodyType.Title;

            EFSingleTon.instance.BodyTypes.Update(oldBodyType);
            EFSingleTon.instance.SaveChanges();
            return true;
        }
        public async Task<bool> DeleteBodyTypeAsync(BodyType bodyType)
        {
            if (bodyType == null || EFSingleTon.instance.BodyTypes.FirstOrDefault(b => b.Id == bodyType.Id) == null)
                return false;

            List<Car> cars = EFSingleTon.instance.Cars.Where(c => c.IdBodyType == bodyType.Id).ToList();

            EFSingleTon.instance.Cars.RemoveRange(cars);
            EFSingleTon.instance.BodyTypes.Remove(bodyType);
            EFSingleTon.instance.SaveChanges();
            return true;
        }


        //Authorization
        public async Task<string> Authorization(string login, string password)
        {
            if (EFSingleTon.instance.Users.FirstOrDefault(u => u.Login == login && u.Password == password) != null)
            {
                AuthorizationUserId = EFSingleTon.instance.Users.FirstOrDefault(u => u.Login == login && u.Password == password).Id;

                return "";
            }
            else
                return "Неправильно введён логин или пароль";
        }

        public async Task<string> RegisterUser(string login, string password)
        {
            if (login == password)
                return "Логин и пароль не должны быть одинаковыми";

            DB.instance.AddUserAsync(new User { Login = login, Password = password });
            return "";
        }
    } 
}

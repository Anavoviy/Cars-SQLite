using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Cars.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        private string login = "";
        private string password = "";
        private string message = "";

        public string Login { get => login; set { login = value; Signal(); } }
        public string Password { get => password; set { password = value; Signal(); } }
        public string Message { get => message; set { message = value; Signal(); } }

        public BaseCommand Registration { get; set; }
        public BaseCommand Authorization { get; set; }

        public AuthorizationViewModel() 
        {
            if (DB.instance.GetCarsAsync().Result.Count == 0 && DB.instance.GetEnginesAsync().Result.Count == 0 && DB.instance.GetBodyTypesAsync().Result.Count == 0)
                AddDataInDB();

            Registration = new BaseCommand(() =>
            {
                Shell.Current.GoToAsync("//Registration");
            });

            Authorization = new BaseCommand(() =>
            {
                if (Login == "" && Password == "")
                {
                    Message = "Введите логин и пароль";
                    return;
                }
                else if (Login != "" && Password == "")
                {
                    Message = "Введите пароль";
                    return;
                }
                else if (Login == "" && Password != "")
                {
                    Message = "Введите логин";
                    return;
                }

                if (DB.instance.Authorization(Login, Password).Result == "")
                {
                    Login = "";
                    Password = "";
                    Message = "";

                    Shell.Current.GoToAsync("//Main");
                }
                else
                    Message = DB.instance.Authorization(Login, Password).Result;
            });
        }

        private async void AddDataInDB()
        {
            DB.instance.AddUserAsync(new User()
            {
                Login = "admin",
                Password = "admin",
                Role = "Admin",
            });


            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Джип 5дв.",
            });
            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Седан",
            });
            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Хэтчбэк 5дв.",
            });
            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Пикап",
            });

            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "J20A",
                HorsePower = 140,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "2JZGE",
                HorsePower = 135,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "L15C",
                HorsePower = 182,
                CylinderArrangement = "I4",
                CylinderCapacity = 1.5,
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "CUMMINS 6.7L",
                HorsePower = 370,
                CylinderArrangement = "I6",
                CylinderCapacity = 6.7,
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "4D20",
                HorsePower = 163,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,
            });

            DB.instance.AddCarAsync(new Car()
            {
                Model = "Suzuki Grand Vitara",
                IdBodyType = DB.instance.GetBodyTypesAsync().Result.FirstOrDefault(b => b.Title == "Джип 5дв.").Id,
                IdEngine = DB.instance.GetEnginesAsync().Result.FirstOrDefault(e => e.Model == "J20A").Id,
                Description = "2007 год",
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Toyota Crown",
                IdBodyType = DB.instance.GetBodyTypesAsync().Result.FirstOrDefault(b => b.Title == "Седан").Id,
                IdEngine = DB.instance.GetEnginesAsync().Result.FirstOrDefault(e => e.Model == "2JZGE").Id,
                Description = "1989 год",
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Honda Civic",
                IdBodyType = DB.instance.GetBodyTypesAsync().Result.FirstOrDefault(b => b.Title == "Хэтчбэк 5дв.").Id,
                IdEngine = DB.instance.GetEnginesAsync().Result.FirstOrDefault(e => e.Model == "L15C").Id,
                Description = "2018 год",
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Dodge Ram",
                IdBodyType = DB.instance.GetBodyTypesAsync().Result.FirstOrDefault(b => b.Title == "Пикап").Id,
                IdEngine = DB.instance.GetEnginesAsync().Result.FirstOrDefault(e => e.Model == "CUMMINS 6.7L").Id,
                Description = "2014 год",
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Great Wall Poer KingKong",
                IdBodyType = DB.instance.GetBodyTypesAsync().Result.FirstOrDefault(b => b.Title == "Пикап").Id,
                IdEngine = DB.instance.GetEnginesAsync().Result.FirstOrDefault(e => e.Model == "4D20").Id,
                Description = "2023 год",
            });
        }

    }
}

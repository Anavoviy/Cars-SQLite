using Cars.Models;
using Cars.Services;
using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cars.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AuthorizationPage : ContentPage, INotifyPropertyChanged
	{
        private string login = "";
        private string password = "";
        private string message = "";

        public event PropertyChangedEventHandler PropertyChanged;
        public void Signal([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Login { get => login; set { login = value; Signal(); } }
        public string Password { get => password; set { password = value; Signal(); } }
        public string Message { get => message; set { message = value; Signal(); } }
        public AuthorizationPage ()
		{
			InitializeComponent ();

            if(DB.instance.GetCarsAsync().Result.Count == 0 && DB.instance.GetEnginesAsync().Result.Count == 0 && DB.instance.GetBodyTypesAsync().Result.Count == 0)
                AddDataInDB();

			BindingContext = this;
		}
        private void Authorization(object sender, EventArgs e)
        {
            if(Login == "" &&  Password == "")
            {
                Message = "Введите логин и пароль";
                return;
            }
            else if (Login != "" && Password == "")
            {
                Message = "Введите пароль";
                return;
            }
            else if(Login == "" && Password != "")
            {
                Message = "Введите логин";
                return;
            }

            if(DB.instance.Authorization(Login, Password).Result == "")
            {
                Login = "";
                Password = "";
                Message = "";

                Shell.Current.GoToAsync("//Main?update=1");
            }
            else
                Message = DB.instance.Authorization(Login, Password).Result;
        }

        private async void AddDataInDB()
        {
            DB.instance.AddUserAsync(new User()
            {
                Login = "admin",
                Password = "admin"
            });


            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Джип 5дв.",
                UserId = 1
            });
            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Седан",
                UserId = 1
            });
            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Хэтчбэк 5дв.",
                UserId = 1
            });
            DB.instance.AddBodyTypeAsync(new BodyType()
            {
                Title = "Пикап",
                UserId = 1
            });

            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "J20A",
                HorsePower = 140,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,

                UserId = 1
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "2JZGE",
                HorsePower = 135,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,

                UserId = 1
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "L15C",
                HorsePower = 182,
                CylinderArrangement = "I4",
                CylinderCapacity = 1.5,

                UserId = 1
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "CUMMINS 6.7L",
                HorsePower = 370,
                CylinderArrangement = "I6",
                CylinderCapacity = 6.7,

                UserId = 1
            });
            DB.instance.AddEngineAsync(new Engine()
            {
                Model = "4D20",
                HorsePower = 163,
                CylinderArrangement = "I4",
                CylinderCapacity = 2.0,

                UserId = 1
            });

            DB.instance.AddCarAsync(new Car()
            {
                Model = "Suzuki Grand Vitara",
                IdBodyType = 1,
                IdEngine = 1,
                Description = "2007 год",

                UserId = 1
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Toyota Crown",
                IdBodyType = 2,
                IdEngine = 2,
                Description = "1989 год",

                UserId = 1
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Honda Civic",
                IdBodyType = 3,
                IdEngine = 3,
                Description = "2018 год",

                UserId = 1
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Dodge Ram",
                IdBodyType = 4,
                IdEngine = 4,
                Description = "2014 год",

                UserId = 1
            });
            DB.instance.AddCarAsync(new Car()
            {
                Model = "Great Wall Poer KingKong",
                IdBodyType = 4,
                IdEngine = 5,
                Description = "2023 год",

                UserId = 1
            });
        }

        private void Registration(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Registration");
        }
    }
}
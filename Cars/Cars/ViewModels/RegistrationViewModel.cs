using Cars.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Cars.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string login;
        private string password;
        private string message;

        public string Login { get => login; set { login = value; Signal(); } }
        public string Password { get => password; set { password = value; Signal(); } }
        public string Message { get => message; set { message = value; Signal(); } }

        public BaseCommand Registration { get; set; }

        public RegistrationViewModel() 
        {
            Registration = new BaseCommand(() =>
            {
                if (Login == "" && Password == "")
                {
                    Message = "Введите логин и пароль";
                    return;
                }
                else if (Login == "" && Password != "")
                {
                    Message = "Введите логин";
                    return;
                }
                else if (Login != "" && Password == "")
                {
                    Message = "Введите пароль";
                    return;
                }

                if (DB.instance.RegisterUser(Login, Password).Result == "")
                {
                    Login = "";
                    Password = "";
                    Message = "";

                    Shell.Current.GoToAsync("//Authorization");
                }
                else
                    Message = DB.instance.RegisterUser(Login, Password).Result;
            });
        }

    }
}

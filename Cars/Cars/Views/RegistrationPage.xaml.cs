using Cars.Services;
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
	public partial class RegistrationPage : ContentPage, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        public void Signal([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string login;
        private string password;
        private string message;

        public string Login { get => login; set { login = value; Signal(); } }
        public string Password { get => password; set { password = value; Signal(); } }
        public string Message { get => message; set { message = value; Signal(); } }

        public RegistrationPage ()
		{
			InitializeComponent ();
            BindingContext = this;
		}

        private void Registration(object sender, EventArgs e)
        {
            if(Login == "" && Password == "")
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
        }
    }
}
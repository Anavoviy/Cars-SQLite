using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cars.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            InitializeComponent();
        }

        private async void ToRegistration(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Authorization");
        }

        private async void ToAuthorization(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Authorization");
        }
    }
}
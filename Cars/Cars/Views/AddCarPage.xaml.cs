using Cars.Models;
using Cars.Services;
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
    
    public partial class AddCarPage : ContentPage
    {
        

        public AddCarPage()
        {
            InitializeComponent();
        }
    }
}
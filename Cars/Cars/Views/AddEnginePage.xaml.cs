using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cars.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEnginePage : ContentPage
    {
        public AddEnginePage()
        {
            InitializeComponent();
        }
    }
}
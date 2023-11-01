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
    [QueryProperty(nameof(CarId), "id")]
    public partial class AddCarPage : ContentPage
    {
        private int carId;

        public int CarId { get => carId; 
            set 
            { 
                carId = value; 
                if(carId == 0)
                    BindingContext = new AddCarViewModel();
                else
                {
                    Car car = DB.instance.GetCarAsync(carId).Result;
                    if(car != null)
                    {
                        BindingContext = new AddCarViewModel(car);
                    }
                }
            } }

        public AddCarPage()
        {
            InitializeComponent();
        }
    }
}
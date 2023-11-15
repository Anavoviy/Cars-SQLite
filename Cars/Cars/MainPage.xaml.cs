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

namespace Cars
{

    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void CarsVisible(object sender, EventArgs e)
        {
            EnginesButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#EEEEEE");
            BodyTypesButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#EEEEEE");
            CarsButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#FF5722");

            CollectionCars.IsVisible = true;
            CollectionBodyTypes.IsVisible = false;
            CollectionEngines.IsVisible = false;

            AddCar.IsVisible = true;
            AddEngine.IsVisible = false;
            AddBodyType.IsVisible = false;

            new BaseViewModel().Signal();
        }

        private void BodyTypesVisible(object sender, EventArgs e)
        {
            EnginesButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#EEEEEE");
            BodyTypesButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#FF5722");
            CarsButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#EEEEEE");

            CollectionCars.IsVisible = false;
            CollectionBodyTypes.IsVisible = true;
            CollectionEngines.IsVisible = false;

            AddCar.IsVisible = false;
            AddEngine.IsVisible = false;
            AddBodyType.IsVisible = true;
            
            new BaseViewModel().Signal();
        }

        private void EnginesVisible(object sender, EventArgs e)
        {
            EnginesButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#FF5722");
            BodyTypesButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#EEEEEE");
            CarsButton.TextColor = new SolidColorBrush().Color = Color.FromHex("#EEEEEE");

            CollectionCars.IsVisible = false;
            CollectionBodyTypes.IsVisible = false;
            CollectionEngines.IsVisible = true;

            AddCar.IsVisible = false;
            AddEngine.IsVisible = true;
            AddBodyType.IsVisible = false;

            new BaseViewModel().Signal();
        }

        protected override void OnAppearing() => ((MainViewModel)this.BindingContext).OnAppearing();
       
    }
}

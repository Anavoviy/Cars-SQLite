using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cars.ViewModels
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public class AddCarViewModel : BaseViewModel
    {
        public Car EditCar { get; set; } = new Car();

        private List<Engine> engines;
        private List<BodyType> bodyTypes;
        private Engine selectedEngine;
        private BodyType selectedBodyType;

        public List<Engine> Engines { get => engines; set { engines = value; Signal(); } }
        public List<BodyType> BodyTypes { get => bodyTypes; set { bodyTypes = value; Signal(); } }

        public Engine SelectedEngine { get => selectedEngine; set { selectedEngine = value; EditCar.IdEngine = SelectedEngine.Id; } }
        public BodyType SelectedBodyType { get => selectedBodyType; set { selectedBodyType = value; EditCar.IdBodyType = SelectedBodyType.Id; } }

        public BaseCommandParameter Save { get; set; } = new BaseCommandParameter(async (obj) => {

            Car car = obj as Car;
            if(car == null || car.IdEngine == 0 || car.IdBodyType == 0)
                return;

            if(car.Id == 0 )
                await DB.instance.AddCarAsync(car);
            else
                await DB.instance.UpdateCarAsync(car);

            Exit.Execute();
        });
        public static BaseCommand Exit { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync("///Main?update=1");  
        });

        public AddCarViewModel()
        {
            Engines = DB.instance.GetEnginesAsync().Result;
            BodyTypes = DB.instance.GetBodyTypesAsync().Result;
        }
        public AddCarViewModel(Car car)
        {
            Engines = DB.instance.GetEnginesAsync().Result;
            BodyTypes = DB.instance.GetBodyTypesAsync().Result;

            SelectedEngine = DB.instance.GetEngineAsync(car.IdEngine).Result;
            SelectedBodyType = DB.instance.GetBodyTypeAsync(car.IdBodyType).Result;

            EditCar = car;

            Signal();
        }
    }
}

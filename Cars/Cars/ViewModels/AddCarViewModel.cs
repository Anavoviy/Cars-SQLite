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
    [QueryProperty(nameof(CarId), "id")]
    public class AddCarViewModel : BaseViewModel
    {
        private int carId;

        public int CarId
        {
            get => carId;
            set
            {
                carId = value;
                if (carId == 0)
                    EditCar = new Car();
                else
                {
                    Car car = DB.instance.GetCarAsync(carId).Result;
                    if (car != null)
                    {
                        EditCar = car;

                        SelectedEngine = DB.instance.GetEngineAsync(car.IdEngine).Result;
                        SelectedBodyType = DB.instance.GetBodyTypeAsync(car.IdBodyType).Result;

                        Signal();
                    }
                }
            }
        }

        public Car EditCar { get => editCar; set { editCar = value; Signal(); } }
        private List<Engine> engines;
        private List<BodyType> bodyTypes;
        private Engine selectedEngine;
        private BodyType selectedBodyType;
        private Car editCar = new Car();

        public List<Engine> Engines { get => engines; set { engines = value; Signal(); } }
        public List<BodyType> BodyTypes { get => bodyTypes; set { bodyTypes = value; Signal(); } }

        public Engine SelectedEngine { get => selectedEngine; set { selectedEngine = value; EditCar.IdEngine = SelectedEngine.Id; Signal(); } }
        public BodyType SelectedBodyType { get => selectedBodyType; set { selectedBodyType = value; EditCar.IdBodyType = SelectedBodyType.Id; Signal(); } }

        public BaseCommand Save { get; set; } 
        public static BaseCommand Exit { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync("///Main");  
        });

        public AddCarViewModel()
        {
            Engines = DB.instance.GetEnginesAsync().Result;
            BodyTypes = DB.instance.GetBodyTypesAsync().Result;

            Save = new BaseCommand(async () => {

                Car car = EditCar;
                if (car == null || car.IdEngine == 0 || car.IdBodyType == 0)
                    return;

                if (car.Id == 0)
                    await DB.instance.AddCarAsync(car);
                else
                    await DB.instance.UpdateCarAsync(car);

                Exit.Execute();
            });
        }
    }
}

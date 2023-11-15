using Cars.Models;
using Cars.Services;
using Cars.Views;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using BodyType = Cars.Models.BodyType;

namespace Cars.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private List<Car> cars;
        private List<BodyType> bodyTypes;
        private List<Engine> engines;

        public List<Car> Cars { get => cars; set { cars = value; Signal(); } }
        public List<BodyType> BodyTypes { get => bodyTypes; set { bodyTypes = value; Signal(); } }
        public List<Engine> Engines { get => engines; set { engines = value; Signal(); } }

        public Car SelectedCar { get; set; }
        public Engine SelectedEngine { get; set; }
        public BodyType SelectedBodyType { get; set; }


        public BaseCommand AddCar { get; set; }
        public BaseCommand AddEngine { get; set; }
        public BaseCommand AddBodyType { get; set; }

        public BaseCommandParameter EditCar { get; set; }
        public BaseCommandParameter EditEngine { get; set; }
        public BaseCommandParameter EditBodyType { get; set; }

        public BaseCommandParameter DeleteCar { get; set; }
        public BaseCommandParameter DeleteEngine { get; set; }
        public BaseCommandParameter DeleteBodyType { get; set; }

        public MainViewModel() 
        {
            Cars = DB.instance.GetCarsAsync().Result.ToList();
            BodyTypes = DB.instance.GetBodyTypesAsync().Result.ToList();
            Engines = DB.instance.GetEnginesAsync().Result.ToList();

            Routing.RegisterRoute("EditCar", typeof(AddCarPage));
            Routing.RegisterRoute("EditEngine", typeof(AddEnginePage));
            Routing.RegisterRoute("EditBodyType", typeof(AddBodyTypePage));

            DeleteBodyType = new BaseCommandParameter(async (arg) =>
            {
                if (arg != null && arg as BodyType != null)
                {
                    BodyType bodyType = (BodyType)arg;
                    DB.instance.DeleteBodyTypeAsync(bodyType);
                }

                OnAppearing();

                AddBodyTypeViewModel.Exit.Execute();
            }, () => DB.instance.UserIsAdmin);
            DeleteEngine = new BaseCommandParameter(async (arg) =>
            {
                if (arg != null && arg as Engine != null)
                {
                    Engine engine = (Engine)arg;
                    await DB.instance.DeleteEngineAsync(engine);
                }

                OnAppearing();

                AddEngineViewModel.Exit.Execute();
            }, () => DB.instance.UserIsAdmin);
            DeleteCar = new BaseCommandParameter(async (arg) =>
            {
                if (arg != null && arg as Car != null)
                {
                    Car car = (Car)arg;
                    await DB.instance.DeleteCarAsync(car);
                }

                OnAppearing();

                AddCarViewModel.Exit.Execute();
            }, () => DB.instance.UserIsAdmin);

            AddCar = new BaseCommand(async () =>
            {
                await Shell.Current.GoToAsync($"EditCar?id={0}");
            }, () => DB.instance.UserIsAdmin);
            AddEngine = new BaseCommand(async () =>
            {
                await Shell.Current.GoToAsync($"EditEngine?id={0}");
            }, () => DB.instance.UserIsAdmin);
            AddBodyType = new BaseCommand(async () =>
            {
                await Shell.Current.GoToAsync($"EditBodyType?id={0}");
            }, () => DB.instance.UserIsAdmin);

            EditCar = new BaseCommandParameter(async (arg) =>
            {
                await Shell.Current.GoToAsync($"EditCar?id={(int)arg}");
            }, () => DB.instance.UserIsAdmin);
            EditEngine = new BaseCommandParameter(async (arg) =>
            {
                await Shell.Current.GoToAsync($"EditEngine?id={(int)arg}");
            }, () => DB.instance.UserIsAdmin);
            EditBodyType = new BaseCommandParameter(async (arg) =>
            {
                await Shell.Current.GoToAsync($"EditBodyType?id={(int)arg}");
            }, () => DB.instance.UserIsAdmin);

            Signal();
        }

        public void OnAppearing()
        {
            Cars = DB.instance.GetCarsAsync().Result.ToList();
            BodyTypes = DB.instance.GetBodyTypesAsync().Result.ToList();
            Engines = DB.instance.GetEnginesAsync().Result.ToList();

            Signal();
        }
    }
}

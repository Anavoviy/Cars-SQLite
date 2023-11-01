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



        public BaseCommand AddCar { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync($"EditCar?id={0}");
        });
        public BaseCommand AddEngine { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync($"EditEngine?id={0}");
        });
        public BaseCommand AddBodyType { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync($"EditBodyType?id={0}");
        });

        public static BaseCommandParameter DeleteCar { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            if (arg != null && arg as Car != null)
            {
                Car car = (Car)arg;
                await DB.instance.DeleteCarAsync(car);
            }
            AddCarViewModel.Exit.Execute();
        });
        public static BaseCommandParameter DeleteEngine { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            if (arg != null && arg as Engine != null)
            {
                Engine engine = (Engine)arg;
                await DB.instance.DeleteEngineAsync(engine);
            }
            AddEngineViewModel.Exit.Execute();
        });
        public static BaseCommandParameter DeleteBodyType { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            if(arg != null && arg as BodyType != null)
            {
                BodyType bodyType = (BodyType)arg;
                DB.instance.DeleteBodyTypeAsync(bodyType);
            }
            AddBodyTypeViewModel.Exit.Execute();
        });

        public MainViewModel() 
        {

            Cars = DB.instance.GetCarsAsync().Result.ToList();
            BodyTypes = DB.instance.GetBodyTypesAsync().Result.ToList();
            Engines = DB.instance.GetEnginesAsync().Result.ToList();

            Routing.RegisterRoute("EditCar", typeof(AddCarPage));
            Routing.RegisterRoute("EditEngine", typeof(AddEnginePage));
            Routing.RegisterRoute("EditBodyType", typeof(AddBodyTypePage));

            Signal();
        }

        
    }
}

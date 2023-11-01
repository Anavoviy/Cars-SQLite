using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cars.ViewModels
{
    public class AddEngineViewModel : BaseViewModel
    {
        public Engine EditEngine { get; set; } = new Engine();

        public string Model { get; set;}

        public BaseCommandParameter Save { get; set; } = new BaseCommandParameter(async (obj) => {

            Engine engine = obj as Engine;
            if (engine == null || engine.CylinderArrangement == "" || engine.CylinderArrangement == null || engine.Model == "" || engine.Model == null)
                return;

            if (engine.Id == 0)
                await DB.instance.AddEngineAsync(engine);
            else
                await DB.instance.UpdateEngineAsync(engine);

            AddEngineViewModel.Exit.Execute();
        });
        public static BaseCommand Exit { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync("///Main?update=1");
        });

        public AddEngineViewModel()
        {
        }
        public AddEngineViewModel(Engine engine)
        {
            EditEngine = engine;

            Signal();
        }
    }
}

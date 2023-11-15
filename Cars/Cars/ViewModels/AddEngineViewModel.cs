using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cars.ViewModels
{
    [QueryProperty(nameof(EngineId), "id")]
    public class AddEngineViewModel : BaseViewModel
    {
        private int engineId;
        private Engine editEngine = new Engine();

        public int EngineId
        {
            get => engineId;
            set
            {
                engineId = value;
                if (engineId == 0)
                    EditEngine = new Engine();
                else
                {
                    Engine engine = DB.instance.GetEngineAsync(engineId).Result;
                    if (engine != null)
                        EditEngine = engine;
                }
            }
        }

        public Engine EditEngine { get => editEngine; set { editEngine = value; Signal(); } }
        public string Model { get; set;}

        public BaseCommand Save { get; set; }
        public static BaseCommand Exit { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync("///Main");
        });

        public AddEngineViewModel()
        {
            Save = new BaseCommand(async () => {

                Engine engine = EditEngine;
                if (engine == null || engine.CylinderArrangement == "" || engine.CylinderArrangement == null || engine.Model == "" || engine.Model == null)
                    return;

                if (engine.Id == 0)
                    await DB.instance.AddEngineAsync(engine);
                else
                    await DB.instance.UpdateEngineAsync(engine);

                AddEngineViewModel.Exit.Execute();
            });
        }
        
    }
}

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
    [QueryProperty(nameof(EngineId), "id")]
    public partial class AddEnginePage : ContentPage
    {
        private int engineId;

        public int EngineId { get => engineId; 
            set 
            { 
                engineId = value; 
                if(engineId == 0)
                    BindingContext = new AddEngineViewModel();
                else
                {
                    Engine engine = DB.instance.GetEngineAsync(engineId).Result;
                    if(engine != null )
                        BindingContext = new AddEngineViewModel(engine);
                }
            } }


        public AddEnginePage()
        {
            InitializeComponent();
        }

    }
}
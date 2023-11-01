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
    [QueryProperty(nameof(BodyTypeId), "id")]
    public partial class AddBodyTypePage : ContentPage
    {
        private int bodyTypeId;

        public int BodyTypeId
        {
            get => bodyTypeId;
            set
            {
                bodyTypeId = value;
                if (bodyTypeId == 0)
                    BindingContext = new AddBodyTypeViewModel();
                else
                {
                    BodyType bodyType = DB.instance.GetBodyTypeAsync(bodyTypeId).Result;
                    if (bodyType != null)
                    {
                        BindingContext = new AddBodyTypeViewModel(bodyType);
                    }
                }
            }
        }

        public AddBodyTypePage()
        {
            InitializeComponent();
            BindingContext = new AddBodyTypeViewModel();
        }
        
    }
}
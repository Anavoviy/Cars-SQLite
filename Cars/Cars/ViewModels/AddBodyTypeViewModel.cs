using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cars.ViewModels
{
    public class AddBodyTypeViewModel : BaseViewModel
    {
        public BodyType EditBodyType { get; set; } = new BodyType();


        public BaseCommandParameter Save { get; set; } = new BaseCommandParameter(async (obj) => {

            BodyType bodyType = obj as BodyType;
            if (bodyType == null || bodyType.Title == "" || bodyType.Title == null)
                return;

            if (bodyType.Id == 0)
                await DB.instance.AddBodyTypeAsync(bodyType);
            else
                await DB.instance.UpdateBodyTypeAsync(bodyType);

            AddBodyTypeViewModel.Exit.Execute();
        });
        public static BaseCommand Exit { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync("///Main?update=1");
        });

        public AddBodyTypeViewModel()
        {
        }
        public AddBodyTypeViewModel(BodyType bodyType)
        {
            EditBodyType = bodyType;
            Signal();
        }

    }
}

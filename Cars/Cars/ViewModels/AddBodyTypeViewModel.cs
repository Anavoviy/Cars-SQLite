using Cars.Models;
using Cars.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cars.ViewModels
{

    [QueryProperty(nameof(BodyTypeId), "id")]
    public class AddBodyTypeViewModel : BaseViewModel
    {
        private BodyType editBodyType = new BodyType();

        private int bodyTypeId;

        public int BodyTypeId
        {
            get => bodyTypeId;
            set
            {
                bodyTypeId = value;
                if (bodyTypeId == 0)
                    EditBodyType = new BodyType();
                else
                {
                    BodyType bodyType = DB.instance.GetBodyTypeAsync(bodyTypeId).Result;
                    if (bodyType != null)
                    {
                        EditBodyType = bodyType;
                    }
                }
            }
        }

        public BodyType EditBodyType { get => editBodyType; set { editBodyType = value; Signal(); } }

        public BaseCommand Save { get; set; }
        public static BaseCommand Exit { get; set; } = new BaseCommand(async () =>
        {
            await Shell.Current.GoToAsync("///Main");
        });

        public AddBodyTypeViewModel()
        {
            Save = new BaseCommand(async () => {

                BodyType bodyType = EditBodyType;
                if (bodyType == null || bodyType.Title == "" || bodyType.Title == null)
                    return;

                if (bodyType.Id == 0)
                    await DB.instance.AddBodyTypeAsync(bodyType);
                else
                    await DB.instance.UpdateBodyTypeAsync(bodyType);

                AddBodyTypeViewModel.Exit.Execute();
            });
        }
        

    }
}

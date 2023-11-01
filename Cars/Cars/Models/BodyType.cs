using Cars.Services;
using Cars.ViewModels;
using Cars.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xamarin.Forms;

namespace Cars.Models
{
    public class BodyType
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }

        [NotMapped]
        public BaseCommandParameter DeleteBodyType { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            MainViewModel.DeleteBodyType.Execute(arg);
        });
        [NotMapped]
        public BaseCommandParameter EditBodyType { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            await Shell.Current.GoToAsync($"/EditBodyType?id={((BodyType)arg).Id}");
        });
    }
}

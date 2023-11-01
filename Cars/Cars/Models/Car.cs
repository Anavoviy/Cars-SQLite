﻿using Cars.Services;
using Cars.ViewModels;
using Cars.Views;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

namespace Cars.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Model { get; set; } = "";
        public string Description { get; set; } = "";
        public int IdBodyType { get; set; }
        public int IdEngine { get; set; }
        public string EngineView { get; set; } = "";
        public string BodyTypeView { get; set; } = "";

        [NotMapped]
        public BaseCommandParameter DeleteCar { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            MainViewModel.DeleteCar.Execute(arg);
        });
        [NotMapped]
        public BaseCommandParameter EditCar { get; set; } = new BaseCommandParameter(async (arg) =>
        {
            await Shell.Current.GoToAsync($"/AddCarPage?id={((Car)arg).Id}");
        });
    }
}
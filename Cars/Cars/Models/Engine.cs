using Cars.Services;
using Cars.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xamarin.Forms;

namespace Cars.Models
{
    public class Engine
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public double CylinderCapacity { get; set; }
        public string CylinderArrangement { get; set; }
        public int HorsePower { get; set; }

        public override string ToString()
        {
            return Model + " Компоновка: " + CylinderArrangement + " Объём: " + CylinderCapacity + "л." + " Мощность: " + HorsePower + "л.с.";
        }
    }
}

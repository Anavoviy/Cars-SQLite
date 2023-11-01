using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cars.Interfaces;
using Cars.Droid;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(AndroidDBPath))]
namespace Cars.Droid
{
    public class AndroidDBPath : IDBPath
    {
        public AndroidDBPath()
        {
        }

        public string GetPath(string fileName)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
        }
    }
}
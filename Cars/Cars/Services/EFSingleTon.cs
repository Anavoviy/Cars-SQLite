using Cars.Interfaces;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Cars.Services
{
    public class EFSingleTon
    {
        private static DataBaseContext _context;

        public static DataBaseContext instance
        {
            get
            {
                if (_context == null)
                {
                    string filename = "dataBaseCars.db";
                    string path = DependencyService.Get<IDBPath>().GetPath(filename);

                    _context = new DataBaseContext(path);
                    _context.Database.EnsureCreated();
                }
                return _context;
            }
            set => _context = value;
        }
        
    }
}

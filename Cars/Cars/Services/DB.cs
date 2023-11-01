using Cars.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Services
{
    public static class DB
    {
        private static DataBase instance1;

        public static DataBase instance { 
            get 
            { 
                if(instance1 == null)
                    instance1 = new DataBase();
                
                return instance1; 
            } 
            set => instance1 = value; }
    }
}

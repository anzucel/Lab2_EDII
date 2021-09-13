using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoAPI;
using ProyectoAPI.Models;

namespace ProyectoAPI.Models
{
    public sealed class Singleton
    {
        private readonly static Singleton instance = new Singleton();

        private Singleton()
        {
           
        }
      
        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }
    }
}

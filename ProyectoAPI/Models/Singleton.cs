using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoAPI;
using ProyectoAPI.Models;
using Huffman;
 

namespace ProyectoAPI.Models
{
    public sealed class Singleton
    {
        private readonly static Singleton instance = new Singleton();
        public ListaDobleEnlace.ListaDoble<Compresiones> Historial = new ListaDobleEnlace.ListaDoble<Compresiones>();
        public IHuffman huffman_CD;

        public string cadena_texto;
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

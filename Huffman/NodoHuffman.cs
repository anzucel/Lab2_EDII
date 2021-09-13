using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    public class NodoHuffman
    {
        public int valor { get; set; }
        public char caracter { get; set; }
        //public int altura { get; set; }

        //Posiciones del árbol binario
        //public NodoHuffman derecha { get; set; }
        //public NodoHuffman izquierda { get; set; }


        // constructor de la clase Nodo
        public NodoHuffman()
        {
           // altura = 0;
            //derecha = null;
            //izquierda = null;
        }

        // public Nodo<T> raiz; 

        ~NodoHuffman() { }
    }
}

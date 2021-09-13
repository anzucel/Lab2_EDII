using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    public class Heap: NodoHuffman
    {
        private NodoHuffman[] ArregloHeap { get; set; }

        public int max { get; set; }
        public int contador { get; set; }

        public Heap(int m) {
            max = m;
            ArregloHeap = new NodoHuffman[max];
            contador = 0;
        }

        private static void Swap(ref NodoHuffman p, ref NodoHuffman q)
        {
            NodoHuffman temp = p;
            p = q;
            q = temp;
        }

        private int ObtenerPadre(int posicion)
        {
            int aux = (posicion - 1) / 2;
            if (posicion == 0)
            {
                return -1;
            }
            else
            {
                return aux;
            }
        }

        private int ObtenerIzq(int posicion)
        {
            int aux = 2 * posicion + 1;
            if (aux < contador)
            {
                return aux;
            }
            else
            {
                return -1;
            }
        }

        private int ObtenerDer(int posicion)
        {
            int aux = 2 * posicion + 2;
            if (aux < contador)
            {
                return aux;
            }
            else
            {
                return -1;
            }
        }

        public bool Insertar(NodoHuffman nuevo)
        {
            if (contador == max)
            {
                return false;
            }

            //Inserta el nuevo valor al final
            int i = contador;
            ArregloHeap[i] = nuevo;
            contador++;


            while (i != 0 && ArregloHeap[i].valor < ArregloHeap[ObtenerPadre(i)].valor)
            {
                Swap(ref ArregloHeap[i], ref ArregloHeap[ObtenerPadre(i)]);
                i = ObtenerPadre(i);
            }
            return true;
        }

        /*decrease key
        private void Ordenar(int posicion, NodoHuffman nuevo)
        {
            ArregloHeap[posicion] = nuevo;

            while (posicion != 0 && ArregloHeap[posicion].valor < ArregloHeap[ObtenerPadre(posicion)].valor)
            {
                Swap(ref ArregloHeap[posicion], ref ArregloHeap[ObtenerPadre(posicion)]);
                posicion = ObtenerPadre(posicion);
            }
        }*/

        public NodoHuffman Obtener()
        {
            return ArregloHeap[0];
        }

        public NodoHuffman Extraer()
        {
            if (contador <= 0)
            {
                return null;
            }

            if (contador == 1)
            {
                contador--;
                return ArregloHeap[0];
            }

            NodoHuffman raiz = ArregloHeap[0];
            ArregloHeap[0] = ArregloHeap[contador - 1];
            contador--;
            OrdenarMin(0); //MinHeapify

            return raiz;
        }

        //MinHeapify
        private void OrdenarMin(int posicion)
        {
            int izq = ObtenerIzq(posicion);
            int der = ObtenerDer(posicion);

            if (izq >= 0 && der >= 0 && ArregloHeap[izq].valor > ArregloHeap[der].valor)
            {
                izq = der;
            }
            if (izq > 0 && ArregloHeap[posicion].valor > ArregloHeap[izq].valor)
            {
                NodoHuffman aux = ArregloHeap[posicion];
                ArregloHeap[posicion] = ArregloHeap[izq];
                ArregloHeap[izq] = aux;
                OrdenarMin(izq);
            }
            /*int menor = posicion;
            if (izq < contador && ArregloHeap[izq].valor < ArregloHeap[menor].valor)
            {
                menor = 1;
            }
            if (der < contador && ArregloHeap[der].valor < ArregloHeap[menor].valor)
            {
                menor = der;
            }

            if (menor != posicion)
            {
                Swap(ref ArregloHeap[posicion], ref ArregloHeap[menor]);
                OrdenarMin(menor);
            }*/
        }

        /*
        private void OrdenarMax(int posicion, NodoHuffman nuevo)
        {
            ArregloHeap[posicion] = nuevo;
            OrdenarMin(posicion);
        }*/
    }
}

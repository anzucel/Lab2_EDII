using System;
using ListaDobleEnlace;

namespace Huffman
{
    public class Huffman : IHuffman
    {
        //texto original 
        string Texto { get; set; }
        char[] ArrayTexto { get; set; } //texto completo 

        ListaDoble<char> Caracteres { get; set; } // caracteres que contiene el texto 
        int[] Frecuencia { get; set; }  // cantidad de cada letra en el texto

        //constructor, recibe texto que será compreso/descompreso
        public Huffman(string texto_comprimir)
        {
            Texto = texto_comprimir;
            ArrayTexto = Texto.ToCharArray();    //Texto a arreglo
            Caracteres = new ListaDoble<char>();   
        }

        private void ExtraerCaracteres()
        {
            bool insertar; 
            for (int i = 0; i < ArrayTexto.Length; i++)
            {
                insertar = true;
                for (int j = 0; j < Caracteres.contador; j++)
                {
                    if (ArrayTexto[i] == Caracteres.ObtenerValor(j))
                    {
                        insertar = false;
                        break;
                    }
                }

                if (insertar)
                {
                    Caracteres.InsertarFinal(ArrayTexto[i]);
                }
            }
        }

        private void ContarCaracteres()
        {
            Frecuencia = new int[Caracteres.contador];

            for (int i = 0; i < Frecuencia.Length; i++)
            {
                Frecuencia[i] = 0;
            }

            for (int i = 0; i < Caracteres.contador; i++)
            {
                for (int j = 0; j < ArrayTexto.Length; j++)
                {
                    if (Caracteres.ObtenerValor(i) == ArrayTexto[j])
                    {
                        Frecuencia[i]++;
                    }
                }
            }
        }

        private void BubbleSort()
        {
            for (int i = 0; i < Frecuencia.Length - 1; i++)
            {
                for (int j = i + 1; j < Frecuencia.Length; j++)
                {
                    if (Frecuencia[i] > Frecuencia[j])
                    {
                        int temp = Frecuencia[i];
                        Frecuencia[i] = Frecuencia[j];
                        Frecuencia[j] = temp;

                        char temp2 = Caracteres.ExtraerEnPosicion(i).Valor;
                        Caracteres.InsertarEnPosicion(Caracteres.ExtraerEnPosicion(j - 1).Valor, i);
                        Caracteres.InsertarEnPosicion(temp2, j);
                    }
                }
            }
        }

        //Métodos de la interfaz
        public string Comprimir()
        {
            //se buscan los diferentes caracteres que contiene el texto 
            ExtraerCaracteres();
            ContarCaracteres();
            BubbleSort();

            Arbol Heap = new Arbol();

            for (int i = 0; i < Caracteres.contador; i++)
            {
                NodoHuffman nodo = new NodoHuffman();

                nodo.valor = Frecuencia[i];
                nodo.caracter = Caracteres.ObtenerValor(i);

                //nodo.derecha = null;
                //nodo.izquierda = null;

                Heap.Insertar(nodo);
            }

            ListaDoble<char> prueba = new ListaDoble<char>();

            while (Heap != null)
            {
                char nuevo = Heap.Eliminar().caracter;
            }

            throw new NotImplementedException();
        }

        public string Descomprimir()
        {
            throw new NotImplementedException();
        }
    }
}

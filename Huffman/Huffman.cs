using System;
using ListaDobleEnlace;

namespace Huffman
{
    public class Huffman : IHuffman
    {
        //texto original 
        string Texto { get; set; }
        char[] ArrayTexto { get; set; } //texto completo 
        ListaDoble<NodoHuffman> Conteo { get; set; }

        //constructor, recibe texto que será compreso/descompreso
        public Huffman(string texto_comprimir)
        {
            Texto = texto_comprimir;
            ArrayTexto = Texto.ToCharArray();    //Texto a arreglo
            Conteo = new ListaDoble<NodoHuffman>();
        }

        private void ExtraerCaracteres()
        {
            bool insertar; 
            for (int i = 0; i < ArrayTexto.Length; i++)
            {
                insertar = true;
                for (int j = 0; j < Conteo.contador; j++)
                {
                    if (ArrayTexto[i] == Conteo.ObtenerValor(j).caracter)
                    {
                        insertar = false;
                        break;
                    }
                }

                if (insertar)
                {
                    NodoHuffman nodo = new NodoHuffman();
                    nodo.caracter = ArrayTexto[i];
                    Conteo.InsertarFinal(nodo);
                }
            }
        }

        private void ContarCaracteres()
        {
            for (int i = 0; i < Conteo.contador; i++)
            {
                for (int j = 0; j < ArrayTexto.Length; j++)
                {
                    if (Conteo.ObtenerValor(i).caracter == ArrayTexto[j])
                    {
                        NodoHuffman nuevo = new NodoHuffman();
                        NodoHuffman nodo = Conteo.ExtraerEnPosicion(i).Valor;

                        nuevo.caracter = nodo.caracter;
                        nuevo.valor = nodo.valor;
                        nuevo.valor++;

                        Conteo.InsertarEnPosicion(nuevo, i);
                    }
                }
            }
        }

        public void GenerarPrefijos(NodoHuffman raiz, string codigo)
        {
            if (raiz.izquierda == null && raiz.derecha == null && raiz.caracter != '^')
            {
                for (int i = 0; i < Conteo.contador; i++)
                {
                    if (Conteo.ObtenerValor(i).caracter == raiz.caracter)
                    {
                        NodoHuffman aux = Conteo.ExtraerEnPosicion(i).Valor;
                        NodoHuffman nuevo = new NodoHuffman();
                        nuevo.caracter = aux.caracter;
                        nuevo.valor = aux.valor;
                        nuevo.prefijo = codigo;
                        Conteo.InsertarEnPosicion(nuevo, i);
                        break;
                    }
                }
            }

            if(raiz.izquierda != null && raiz.derecha != null)
            {
                GenerarPrefijos(raiz.izquierda, codigo + "0");
                GenerarPrefijos(raiz.derecha, codigo + "1");
            }
        }

        //Métodos de la interfaz
        public string Comprimir()
        {
            //se buscan los diferentes caracteres que contiene el texto 
            ExtraerCaracteres();
            ContarCaracteres();
            
            Heap Heap = new Heap(Conteo.contador);

            for (int i = 0; i < Conteo.contador; i++)
            {
                NodoHuffman nodo = new NodoHuffman();

                nodo.valor = Conteo.ObtenerValor(i).valor;
                nodo.caracter = Conteo.ObtenerValor(i).caracter;

                Heap.Insertar(nodo);
            }

            NodoHuffman raiz = null;

            while (Heap.contador > 1)
            {
                NodoHuffman nodo_izq = Heap.Extraer();
                NodoHuffman nodo_der = Heap.Extraer();

                NodoHuffman nodo_pivote = new NodoHuffman();

                //la suma de la frecuencia de los dos nodos
                nodo_pivote.valor = nodo_izq.valor + nodo_der.valor;
                nodo_pivote.caracter = '^';

                nodo_pivote.izquierda = nodo_izq;
                nodo_pivote.derecha = nodo_der;

                raiz = nodo_pivote;

                Heap.Insertar(nodo_pivote);
            }

            GenerarPrefijos(raiz, "");



            ListaDoble<char> prueba = new ListaDoble<char>();

            while (Heap != null)
            {
                char nuevo = Heap.Extraer().caracter;
            }

            throw new NotImplementedException();
        }

        public string Descomprimir()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    public class Arbol : NodoHuffman
    {
        public NodoHuffman raiz;
        public int contador;

        //constructor 
        public Arbol()
        {
            raiz = null;
            contador = 0;
        }

        ~Arbol() { }

        public void Insertar(NodoHuffman nuevo)
        {
            NodoHuffman NuevoNodo = new NodoHuffman();
            NuevoNodo.valor = nuevo.valor;
            NuevoNodo.caracter = nuevo.caracter;
            NuevoNodo.izquierda = null;
            NuevoNodo.derecha = null;

            if (raiz == null)
            {
                raiz = NuevoNodo;
            }
            else
            {
                raiz = InsertarNodo(raiz, NuevoNodo);
            }
            contador++;
        }

        private NodoHuffman InsertarNodo(NodoHuffman actual, NodoHuffman nuevo)
        {
            if (actual == null)
            {
                return nuevo;
            }
            if (actual.caracter.CompareTo(nuevo.caracter) < 0)
            {
                actual.derecha = InsertarNodo(actual.derecha, nuevo);
                if (actual.derecha.valor <= actual.valor)
                {
                    actual = RotarIzquierda(actual);
                }
            }
            else
            {
                actual.izquierda = InsertarNodo(actual.izquierda, nuevo);
                if (actual.izquierda.valor <= actual.valor)
                {
                    actual = RotarDerecha(actual);
                }
            }
            return actual;
        }

        /*public int Buscar(char nombre)
        {
            NodoHuffman recorrer = raiz;
            bool encontrar = false;
            while (recorrer != null && encontrar == false)
            {
                char valor = recorrer.caracter;
                valor = valor.ToLower();
                if (nombre == valor)
                {
                    encontrar = true;
                }
                else
                {
                    if (nombre.CompareTo(recorrer.valor.Titulo) > 0)
                    {
                        recorrer = recorrer.derecha;
                        encontrar = false;
                    }
                    else
                    {
                        recorrer = recorrer.izquierda;
                        encontrar = false;
                    }
                }
            }
            if (recorrer == null)
            {
                return 0;
            }
            return recorrer.valor.Prioridad;
        }*/

        public NodoHuffman Eliminar()
        {
            NodoHuffman eliminar = raiz;

            if (raiz == null)
                return raiz;
            else
            {
                //Si el nodo raiz del árbol tiene dos hijos
                if (raiz.izquierda != null)
                {
                    if (raiz.derecha != null)
                    {
                        //Se valida la prioridad
                        if (raiz.izquierda.valor <= raiz.derecha.valor)
                        {
                            // Método rotar a la derecha
                            raiz = RotarDerecha(raiz);
                            //raiz.derecha = Eliminar();
                            eliminar = Eliminar();
                            raiz = eliminar.derecha;
                        }
                        else
                        {
                            raiz = RotarIzquierda(raiz);
                            //raiz.izquierda = Eliminar();
                            eliminar = Eliminar();
                            raiz = eliminar.izquierda;
                        }
                    }
                    else
                    {
                        NodoHuffman auxiliar = raiz;
                        raiz = raiz.izquierda;
                        //raiz = null;
                        //raiz.caracter = '0';
                        contador--;
                        return auxiliar;
                    }
                }
                else
                {
                    if (raiz.derecha != null)
                    {
                        NodoHuffman auxiliar = raiz;
                        raiz = raiz.derecha;
                        //raiz = null;
                        //raiz.caracter = '0';
                        contador--;
                        return auxiliar;
                    }
                    else
                    {
                        // El nodo es hoja
                        NodoHuffman auxiliar = raiz;
                        raiz = null;
                        contador--;
                        return auxiliar;
                    }
                }
            }
            return eliminar;
        }

        private NodoHuffman RotarDerecha(NodoHuffman nodo)
        {
            NodoHuffman auxilar = nodo.izquierda;
            nodo.izquierda = auxilar.derecha;
            auxilar.derecha = nodo;
            return auxilar;
        }

        private NodoHuffman RotarIzquierda(NodoHuffman nodo)
        {
            NodoHuffman auxiliar = nodo.derecha;
            nodo.derecha = auxiliar.izquierda;
            auxiliar.izquierda = nodo;
            return auxiliar;
        }

        public void Delete()
        {
            raiz = null;
            contador = 0;
        }

    }
}

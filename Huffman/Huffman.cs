using System;
using ListaDobleEnlace;

namespace Huffman
{
    public class Huffman : IHuffman
    {
        //texto original 
        string Texto { get; set; }
        char[] ArrayTexto { get; set; } //texto completo 
        ListaDoble<NodoHuffman> Conteo { get; set; } //Guarda el caracter y la frecuencia

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

        private void BubbleSort()
        {
            for (int i = 0; i < Conteo.contador - 1; i++)
            {
                for (int j = i + 1; j < Conteo.contador; j++)
                {
                    if (Conteo.ObtenerValor(i).valor > Conteo.ObtenerValor(j).valor)
                    {
                        NodoHuffman temp = Conteo.ExtraerEnPosicion(i).Valor;
                        Conteo.InsertarEnPosicion(Conteo.ExtraerEnPosicion(j - 1).Valor, i);
                        Conteo.InsertarEnPosicion(temp, j);

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
            
            Heap Heap = new Heap(Conteo.contador);

            for (int i = 0; i < Conteo.contador; i++)
            {
                NodoHuffman nodo = new NodoHuffman();

                nodo.valor = Conteo.ObtenerValor(i).valor;
                nodo.caracter = Conteo.ObtenerValor(i).caracter;

                Heap.Insertar(nodo);
            }

            NodoHuffman raiz = null;




            ListaDoble<char> prueba = new ListaDoble<char>();

            while (Heap != null)
            {
                char nuevo = Heap.Extraer().caracter;
            }

            throw new NotImplementedException();
        }


        //Parte de descomprimir---------------------------------------------
        public string Descomprimir()
        {
            DescomprimirCaracteres();
            throw new NotImplementedException();
        }

        private void DescomprimirCaracteres()
        {
            
            int Bits =Convert.ToInt32 (ArrayTexto[0]);// Números de bits que se usaron 
            

            int cont = 1; //Letras y sus frecuencias
            while (Convert.ToString(ArrayTexto[cont]) != "\n")
            {
                int valor = 0; //frecuencia del caracter
                NodoHuffman Caracter = new NodoHuffman();
                Caracter.caracter = ArrayTexto[cont];
                cont++;

                for (int i = 0; i < Bits; i++) ;
                {
                    valor = valor + ArrayTexto[cont];
                    cont++;
                }
                Caracter.valor = valor;
                Conteo.InsertarFinal(Caracter);
            }
            cont++;
            Texto = "";
            while (cont < ArrayTexto.Length)
            {
               string txt = DecimalBinario(ArrayTexto[cont]);
                Texto = Texto + txt;
                cont++;
            }
            


        }



            //Binario → Decimal
            int BinarioDecimal(long binario)
        {

            int numero = 0;
            int digito = 0;
            const int DIVISOR = 10;

            for (long i = binario, j = 0; i > 0; i /= DIVISOR, j++)
            {
                digito = (int)i % DIVISOR;
                if (digito != 1 && digito != 0)
                {
                    return -1;
                }
                numero += digito * (int)Math.Pow(2, j);
            }

            return numero;
        }


        //Decimal → Binario
        string DecimalBinario(int numero)
        {

            long binario = 0;

            const int DIVISOR = 2;
            long digito = 0;

            for (int i = numero % DIVISOR, j = 0; numero > 0; numero /= DIVISOR, i = numero % DIVISOR, j++)
            {
                digito = i % DIVISOR;
                binario += digito * (long)Math.Pow(10, j);
            }
            string Binario = binario.ToString();

            if (Binario.Length < 8) //autorelleno de los 8 bits
            {
                int ceros = 8 - Binario.Length;
                for (int i = 0; i < ceros; i++)
                {
                    Binario = "0" + Binario;
                }
            }
            binario = Convert.ToInt64(Binario);
            return Binario;
        }
    }
}

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
        string cadena_binario = "";
        string txtComprimido = "";
        string txtDescomprimido = "";
        int cant_bytes = 0;

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

        private string UnirPrefijos()
        {
            string cadena = "";
            for (int i = 0; i < ArrayTexto.Length; i++)
            {
                for (int j = 0; j < Conteo.contador; j++)
                {
                    if (ArrayTexto[i] == Conteo.ObtenerValor(j).caracter)
                    {
                        cadena += Conteo.ObtenerValor(j).prefijo;
                        break;
                    }
                }
            }
            return cadena;
        }

        private string Codificar(string cadena_prefijos, string Tipo)
        {
            string aux = cadena_prefijos;
            string cadena = "";
            string codigo = "";

            for (int i = 0; i < cadena_prefijos.Length; i +=8)
            {
                if (aux.Length/8 > 0)
                {
                    if (aux.Length > 8)
                    {
                        cadena = aux.Remove(8);
                        aux = aux.Remove(0, 8);
                    }
                    else
                    {
                        cadena = aux;
                    }
                    int dec = BinarioDecimal(Convert.ToInt64(cadena));
                    codigo += Convert.ToChar(dec);
                }
                else
                {
                    int agregar = Math.Abs(aux.Length - 8);
                    if (Tipo != "cadena")
                    {
                        for (int j = 0; j < agregar; j++)
                        {
                            aux = 0 + aux; 
                        }
                    }
                    else
                    {
                        for (int j = 0; j < agregar; j++)
                        {
                            aux =aux + 0 ; 
                        }
                    }
                   int dec = BinarioDecimal(Convert.ToInt64(aux));
                    codigo += Convert.ToChar(dec);
                }
            }
            return codigo;
        }

        private int CantBytes()
        {
            int bits = 8;
            int bytes = 1;
            for (int i = 0; i < Conteo.contador; i++)
            {
                if (Conteo.ObtenerValor(i).valor > Math.Pow(2, bits))
                {
                    bits += 8;
                    bytes++;
                }
            }
            return bytes;
        }

        private string CodInfo()
        {
            string codigo = "";
            for (int i = 0; i < Conteo.contador; i++)
            {
                //caracter 
                char aux = Conteo.ObtenerValor(i).caracter; //M = 77
                string caracter = DecimalBinario(Convert.ToInt32(aux)); // 1001101
                //frecuencia 
                string frecuencia = DecimalBinario(Conteo.ObtenerValor(i).valor); // 10000000

                codigo += caracter + frecuencia;
            }
            return codigo;
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

            // Genera los prefijos de cada caracter
            GenerarPrefijos(raiz, "");

            //Genera cadena con código binario 
            cadena_binario = UnirPrefijos();

            //Separa en bytes y convierte a ascii
            txtComprimido = Codificar(cadena_binario, "cadena");

            //agregar al inicio del archivo las frecuencias de cada caracter
            cant_bytes = CantBytes();

            //cant_bytes + \n + caracter \n + frecuencia 
            string info = DecimalBinario(cant_bytes);/* + "00001010";*/
            info = info + CodInfo() + "00001010";
            info = Codificar(info,"Leyenda");
            txtComprimido = info + txtComprimido;
            return txtComprimido;
        }


        //Parte de descomprimir---------------------------------------------
        public string Descomprimir(string texto)
        {
            Texto = texto;
            ArrayTexto = Texto.ToCharArray();    //Texto a arreglo
            Conteo = new ListaDoble<NodoHuffman>();
            DescomprimirCaracteres();


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

            // Genera los prefijos de cada caracter
            GenerarPrefijos(raiz, "");
            Decodificar();
            return txtDescomprimido;
        }

        string Decodificar()
        {
            int limite = 0;//define mi limite del texto
            string textosub = "";
            bool Encontrar = false;
            for (int i = 0; i < Conteo.contador; i++)
            {
                limite = limite + Conteo.ObtenerValor(i).valor;
            }

            while (limite != 0)
            {
                textosub = textosub + Texto.Substring(0, 1);
                Texto = Texto.Remove(0, 1);


                for (int i = 0; i < Conteo.contador; i++)
                {
                    if (textosub == Conteo.ObtenerValor(i).prefijo)
                    {
                        txtDescomprimido = txtDescomprimido + Conteo.ObtenerValor(i).caracter;
                        limite--;
                        textosub = "";
                        break;
                    }
                }

            }


            return txtDescomprimido;
        }

        private void DescomprimirCaracteres()
        {

            int Bits = Convert.ToInt32(ArrayTexto[0]);// Números de bits que se usaron 


            int cont = 1; //Letras y sus frecuencias
            while (Convert.ToString(ArrayTexto[cont]) != "\n")
            {
                int valor = 0; //frecuencia del caracter
                NodoHuffman Caracter = new NodoHuffman();
                Caracter.caracter = ArrayTexto[cont];
                cont++;

                for (int i = 0; i < Bits; i++) 
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

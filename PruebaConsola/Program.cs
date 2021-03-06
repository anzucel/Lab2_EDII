using System;
using Huffman;

namespace PruebaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            string cadena_texto = "MISSISSIPPI RIVER";
            string msj_comprimido;
            string msj_descomprimido;

            IHuffman huffman;
            huffman = new Huffman.Huffman(cadena_texto);

            cadena_texto = huffman.Comprimir(); //Toma el texto comprimido y lo vuelve a reasignar a las variables ya definidas para descomprimir
            msj_descomprimido = huffman.Descomprimir(cadena_texto);//huffman

            Console.WriteLine("Comprimir: " + cadena_texto);
            Console.WriteLine("Descomprimir: " + msj_descomprimido);
            Console.ReadKey();
        }
    }
}

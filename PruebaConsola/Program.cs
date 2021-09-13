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

            IHuffman huffman = new Huffman.Huffman(cadena_texto);
            msj_comprimido = huffman.Comprimir();//huffman
            msj_descomprimido = huffman.Descomprimir();//huffman
        }
    }
}

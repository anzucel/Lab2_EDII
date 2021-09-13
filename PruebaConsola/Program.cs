using System;
using Huffman;

namespace PruebaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            // string cadena_texto = "MISSISSIPPI\n RIVER  Rio misisipi \n ";
            char caracter = Convert.ToChar(01);
            char caracter1 = Convert.ToChar(49);
            char caracter2 = Convert.ToChar(1);
            char caracter3 = Convert.ToChar(73);
            char caracter4 = Convert.ToChar(04);

            //palabras
            char caracter5 = Convert.ToChar(255);
            char caracter6 = Convert.ToChar(1);

            string cadena1_texto = Convert.ToString(Convert.ToString(caracter)+ Convert.ToString(caracter1) + Convert.ToString(caracter2) + Convert.ToString(caracter3) + Convert.ToString(caracter4)+"\n");
            string cadena_texto = cadena1_texto + Convert.ToString(caracter5) + Convert.ToString(caracter6);
            string msj_comprimido;
            string msj_descomprimido;

            IHuffman huffman = new Huffman.Huffman(cadena_texto);
            //msj_comprimido = huffman.Comprimir();//huffman
            msj_descomprimido = huffman.Descomprimir();//huffman
        }
    }
}

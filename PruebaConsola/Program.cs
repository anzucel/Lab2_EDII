using System;
using Huffman;

namespace PruebaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            string cadena_texto = "Como poco coco como\npoco coco compro\nComo compro poco coco\npoco coco como";//"yo dono rosas, oro no doy";
            string msj_comprimido;
            string msj_descomprimido;

            IHuffman huffman;
            huffman = new Huffman.Huffman(cadena_texto);

            msj_comprimido = huffman.ComprimirLzw();

            cadena_texto = huffman.Comprimir(); //Toma el texto comprimido y lo vuelve a reasignar a las variables ya definidas para descomprimir
            msj_descomprimido = huffman.Descomprimir(cadena_texto);//huffman

            Console.WriteLine("Comprimir: " + cadena_texto);
            Console.WriteLine("Descomprimir: " + msj_descomprimido);
            Console.ReadKey();
        }
    }
}

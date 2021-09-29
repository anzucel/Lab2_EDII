using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    public interface IHuffman
    {
        // Devuelve en una cadena el texto comprimido o descomprimido
        string Comprimir();
        string Descomprimir(string texto);
        string Descomprimir_LZW(string texto);
        string ComprimirLzw();

    }
}

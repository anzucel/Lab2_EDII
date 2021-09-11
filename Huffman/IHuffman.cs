using System;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    interface IHuffman
    {
        void Comprimir(string texto);
        void Descomprimir(string texto);
    }
}

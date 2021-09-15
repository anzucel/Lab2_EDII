using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoAPI.Models;
using Microsoft.Extensions.Logging;
using Huffman;


namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class huffman : ControllerBase
    {
        Encoding utf8 = Encoding.UTF8;

        // GET: api/<Huffman> 
        [HttpGet]
        public IEnumerable<string> Get()
        {
           
            return new string[] { "value1" };
        }



        [HttpGet]
        [Route("compressions")]
        public IEnumerable<string> Getcompressions()
        {
            return new string[] { };
        }


        // POST api/<Huffman>

        [HttpPost]
        [Route("decompress")]
        public IActionResult PostFileDecompress([FromForm] IFormFile File)
        {
            using var archivo = new MemoryStream();
            try
            {
                File.CopyToAsync(archivo);
                var coleccion = Encoding.UTF8.GetString(archivo.ToArray());  //pasa el texto a cadena
                Byte[] texto_bytes = utf8.GetBytes(coleccion); // texto a bytes
                string texto = "";
                texto = Encoding.UTF8.GetString(texto_bytes);
                // Singleton.Instance.huffman_CD = new Huffman.Huffman(coleccion);
                Singleton.Instance.huffman_CD = new Huffman.Huffman(texto);
                string Descompresion = Singleton.Instance.huffman_CD.Descomprimir(texto); 
                escribir(Descompresion,"Descompreso");
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("compress/{name}")]
        public IActionResult PostFileCompress([FromForm] IFormFile File, [FromRoute] string name)
        {
            using var archivo = new MemoryStream();
            try
            {
                File.CopyToAsync(archivo);
                var coleccion = Encoding.UTF8.GetString(archivo.ToArray()); //pasa el texto a cadena 
                Byte[] texto_bytes = utf8.GetBytes(coleccion); // texto a bytes 
                string texto = "";
                texto = Encoding.UTF8.GetString(texto_bytes);
                Singleton.Instance.huffman_CD = new Huffman.Huffman(texto);
                string Compresion = Singleton.Instance.huffman_CD.Comprimir();              
                escribir(Compresion, name);
                //Crear el nuevo archivo .huff
                //agregar a la lista para crear el json
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
           
        }


        ///Metodos guardar
        void escribir(string imprimir, string name)
        {

            name ="../Archivos/"+name + ".txt";//Ruta en donde se guardará con el nombre enviado en el post

            Encoding utf8 = Encoding.UTF8;
            //pasar de string a bytes 
            Byte[] texto_bytes = utf8.GetBytes(imprimir);

            //pasar de bytes a string
            string x = Encoding.UTF8.GetString(texto_bytes);
            try
            {
                //Open the File
                StreamWriter sw = new StreamWriter(name, true, Encoding.UTF8);

                //Write out the numbers 1 to 10 on the same line.
                sw.Write(x);

                //close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }

    }
}

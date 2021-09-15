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
        // GET: api/<Huffman> 
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1" };
        }



        [HttpGet]
        [Route("compressions")]
        public IEnumerable<Compresiones> Getcompressions()
        {
          
            return Singleton.Instance.Historial;
           // return new string[] { };
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
                var coleccion = Encoding.ASCII.GetString(archivo.ToArray());

                Singleton.Instance.huffman_CD = new Huffman.Huffman(coleccion);
                var Descompresion = Singleton.Instance.huffman_CD.Descomprimir(coleccion);

                //buscar el nombre original
                Compresiones nombrecompres = Singleton.Instance.Historial.Where(x => x.NombreCompresion == File.FileName).FirstOrDefault<Compresiones>();
                escribir(Descompresion, nombrecompres.Nombre);
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
                double BArchivoOriginal=0, BArchivo=0;//Variables para calcular el factor y razpn de compresión

                File.CopyToAsync(archivo);
                var coleccion = Encoding.ASCII.GetString (archivo.ToArray());             
                Singleton.Instance.huffman_CD = new Huffman.Huffman(coleccion);
                var Compresion = Singleton.Instance.huffman_CD.Comprimir();
                escribir(Compresion, name);

                //Se generan el factor y razon
                BArchivoOriginal = coleccion.Length;
                BArchivo = Compresion.Length;



                Compresiones nuevo = new Compresiones()
                {
                    Nombre = File.FileName,
                    Ruta = Singleton.Instance.DireccionNombre,
                    NombreCompresion = name+".txt",
                    Factor_Compresion = FactorCompresion(BArchivoOriginal, BArchivo),
                    Razon_Compresion = RazonCompresion(BArchivo, BArchivoOriginal),
                };
                double porcentaje= nuevo.Factor_Compresion * 100;
                nuevo.Porcentaje_reduccion = Convert.ToString(porcentaje + "%");

                Singleton.Instance.Historial.Add(nuevo);
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

            Singleton.Instance.DireccionNombre = "";
            Singleton.Instance.DireccionNombre  = "../Archivos/" + name + ".txt";//Ruta en donde se guardará con el nombre enviado en el post


            string x = imprimir;
            try
            {
                StreamWriter sw = new StreamWriter(Singleton.Instance.DireccionNombre , true, Encoding.ASCII);

                sw.Write(x);
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

        double RazonCompresion(double BArchivo, double BArchivoOriginal)
        {
            double result = Math.Round((BArchivo / BArchivoOriginal), 2, MidpointRounding.ToEven);
            return result;
        }

        double FactorCompresion(double BArchivoOriginal, double BArchivo)
        {
            double result = Math.Round((BArchivoOriginal / BArchivo),2,MidpointRounding.ToEven);
            return result;
        }

    }
}
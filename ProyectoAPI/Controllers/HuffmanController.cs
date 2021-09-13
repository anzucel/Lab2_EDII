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

namespace ProyectoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Huffman : ControllerBase
    {
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
                var coleccion = Encoding.ASCII.GetString(archivo.ToArray());
                //foreach (var nuevo in listaPeliculas)
                //{
                //    Singleton.Instance.ABPeliculas.Insertar(nuevo);
                //}
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
                var coleccion = Encoding.ASCII.GetString(archivo.ToArray());              
                //foreach (var nuevo in listaPeliculas)
                //{
                //    Singleton.Instance.ABPeliculas.Insertar(nuevo);
                //}
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

      
    }
}

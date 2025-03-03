﻿using Microsoft.AspNetCore.Mvc;
using WebApi3.Data.Biblioteca_data;
using WebApi3.Data.Contabilidad_data;
using WebApi3.Models.Biblioteca_models;

namespace WebApi3.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class Biblioteca : ControllerBase
    {
        // GET: api/libros
        [HttpGet("libros")]
        public IActionResult GetLibros()
        {
            try
            {
                var libros = Libros_data.ConsultarLibros();
                if (libros == null || !libros.Any())
                {
                    return NoContent();
                }
                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar libros", error = ex.Message });
            }
        }

        // GET: api/libros/5
        [HttpGet("libros/{isbn}")]
        public IActionResult GetLibro(int isbn)
        {
            try
            {
                var libro = Libros_data.ConsultarLibro(isbn);
                if (libro == null)
                {
                    return NotFound(new { message = $"No se encontró el libro con ISBN {isbn}" });
                }
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar el libro", error = ex.Message });
            }
        }

        // POST: api/libros
        [HttpPost("libros")]
        public IActionResult PostLibro([FromBody] Libros_models libro)
        {
            try
            {
                if (libro == null)
                {
                    return BadRequest(new { message = "Datos del libro inválidos" });
                }

                bool resultado = Libros_data.InsertarLibro(libro);
                if (resultado)
                {
                    return Ok(new { message = "Libro insertado correctamente" });
                }
                return StatusCode(500, new { message = "No se pudo insertar el libro" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al insertar el libro", error = ex.Message });
            }
        }

        // PUT: api/libros/5
        [HttpPut("libros/{isbn}")]
        public bool PutLibro(string isbn, [FromBody] Libros_models libro)
        {
            return Libros_data.ActualizarLibro(isbn, libro);
        }

        // DELETE: api/libros/5
        [HttpDelete("libros/{isbn}")]
        public bool DeleteLibro(string isbn)
        {
            return Libros_data.EliminarLibro(isbn);
        }

        //GET: api/Libro/buscar
        [HttpGet("libros/buscar")]
        public IActionResult GetBuscarLibro([FromQuery] int? isbn = null, [FromQuery] int? autorCodigo = null, [FromQuery] string? titulo = null)
        {
            try
            {
                // Llamar al método de datos con los parámetros opcionales.
                var resultados = Libros_data.BuscarLibro(isbn, autorCodigo, titulo);

                // Verificar si hay resultados.
                if (resultados == null || resultados.Count == 0)
                {
                    // En lugar de retornar "NotFound", devolver "NoContent" para indicar que no hay resultados.
                    return NoContent(); // 204 No Content
                }

                // Devolver resultados con estado 200 OK.
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones: devolver estado 500 con un mensaje de error detallado.
                return StatusCode(500, new { message = "Ocurrió un error inesperado al procesar la solicitud de búsqueda de libros.", error = ex.Message });
            }
        }

        // GET: api/autores
        [HttpGet("autores")]
        public List<Autores_models> GetAutores()
        {
            return Autores_data.ConsultarAutores();
        }

        // GET: api/autores/5
        [HttpGet("autores/{codigo}")]
        public Autores_models? GetAutor(int codigo)
        {
            return Autores_data.ConsultarAutor(codigo);
        }

        // POST: api/autores
        [HttpPost("autores")]
        public bool PostAutor([FromBody] Autores_models autor)
        {
            return Autores_data.InsertarAutor(autor);
        }

        // PUT: api/autores/5
        [HttpPut("autores/{codigo}")]
        public bool PutAutor(int codigo, [FromBody] Autores_models autor)
        {
            Console.WriteLine($"{ autor.Codigo}, {autor.Nombre}, {autor.Apellido}");
            return Autores_data.ActualizarAutor(codigo, autor);
        }

        //GET: api/autores/buscar
        [HttpGet("autores/buscar")]
        public IActionResult GetBuscarAutores([FromQuery] int? codigo = null, [FromQuery] string? nombre = null)
        {
            try
            {
                // Llamar al método de datos con los parámetros opcionales.
                var resultados = Autores_data.BuscarAutor(codigo, nombre);

                // Verificar si hay resultados.
                if (resultados == null || resultados.Count == 0)
                {
                    // En lugar de NotFound, retornar un código 204 con un mensaje apropiado para indicar sin contenido.
                    return NoContent(); // 204 No Content
                }

                // Devolver resultados con estado 200 OK.
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Manejo de errores: devolver 500 con mensaje de error detallado.
                return StatusCode(500, new { message = "Ocurrió un error inesperado al procesar la solicitud.", error = ex.Message });
            }
        }

        // DELETE: api/autores/5
        [HttpDelete("autores/{codigo}")]
        public bool DeleteAutor(int codigo)
        {
            return Autores_data.EliminarAutor(codigo);
        }

        // GET: api/prestamos
        [HttpGet("prestamos")]
        public IActionResult GetPrestamos()
        {
            try
            {
                var prestamos = Prestamos_data.ConsultarPrestamos();
                if (prestamos == null || !prestamos.Any())
                {
                    return NoContent();
                }
                return Ok(prestamos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar préstamos", error = ex.Message });
            }
        }

        // GET: api/prestamos/5
        [HttpGet("prestamos/{numero}")]
        public Prestamos_models? GetPrestamo(int numero)
        {
            return Prestamos_data.ConsultarPrestamo(numero);
        }

        // POST: api/prestamos
        [HttpPost("prestamos")]
        public bool PostPrestamo([FromBody] Prestamos_models prestamo)
        {
            return Prestamos_data.InsertarPrestamo(prestamo);
        }

        // PUT: api/prestamos/5
        [HttpPut("prestamos/{numero}")]
        public bool PutPrestamo(int numero, [FromBody] Prestamos_models prestamo)
        {
            return Prestamos_data.ActualizarPrestamo(numero, prestamo);
        }

        // DELETE: api/prestamos/5
        [HttpDelete("prestamos/{numero}")]
        public bool DeletePrestamo(int numero)
        {
            return Prestamos_data.EliminarPrestamo(numero);
        }

        // GET: api/detalleprestamos
        [HttpGet("detalleprestamos/buscar")]
        public IActionResult GetBuscarDetallePrestamos([FromQuery] int? id = null)
                {
                    try
                    {
                        var resultados = DetallePrestamos_data.ConsultarDetalle(id ?? 0);

                        if (resultados == null)
                        {
                            return NoContent();
                        }

                        return Ok(new List<DetallePrestamos_models> { resultados });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new { message = "Error al buscar detalles del préstamo", error = ex.Message });
                    }
                }


        // GET: api/detalleprestamos/5
        [HttpGet("detalleprestamos/{id}")]
        public DetallePrestamos_models? GetDetallePrestamo(int id)
        {
            return DetallePrestamos_data.ConsultarDetalle(id);
        }

        // POST: api/detalleprestamos
        [HttpPost("detalleprestamos")]
        public bool PostDetallePrestamo([FromBody] DetallePrestamos_models detalle)
        {
            return DetallePrestamos_data.InsertarDetalle(detalle);
        }

        // PUT: api/detalleprestamos/5
        [HttpPut("detalleprestamos/{id}")]
        public bool PutDetallePrestamo(int id, [FromBody] DetallePrestamos_models detalle)
        {
            return DetallePrestamos_data.ActualizarDetalle(id, detalle);
        }

        // DELETE: api/detalleprestamos/5
        [HttpDelete("detalleprestamos/{id}")]
        public bool DeleteDetallePrestamo(int id)
        {
            return DetallePrestamos_data.EliminarDetalle(id);
        }
    }
}

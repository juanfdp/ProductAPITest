using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar productos.
    /// </summary>
    [ApiController]
    [Route("api/products")]
    [Produces("application/json")] // Forzar JSON en todo el controlador
    public class ProductsController : ControllerBase
    {
        private readonly JsonFileService _jsonFileService;
        private static List<Product> Products = new List<Product>();
        private static int NextId = 1;

        public ProductsController(JsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        /// <summary>
        /// Obtiene una lista de todos los productos.
        /// </summary>
        /// <returns>Una lista de productos.</returns>
        // GET: api/products
        [HttpGet]
        [Consumes("application/json")] // Solo acepta JSON en todo el controlador
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _jsonFileService.ReadProducts();
            return Ok(products);
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">El ID del producto.</param>
        /// <returns>El producto correspondiente al ID especificado.</returns>
        /// <response code="200">Devuelve el producto solicitado.</response>
        /// <response code="404">Si el producto no existe.</response>
        // GET: api/products/{id}
        [HttpGet("{id}")]
        [Consumes("application/json")] // Solo acepta JSON en todo el controlador
        public ActionResult<Product> Get(int id)
        {
            var products = _jsonFileService.ReadProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="product">Los datos del producto a crear.</param>
        /// <returns>El producto recién creado.</returns>
        /// <response code="201">Devuelve el producto creado.</response>
        /// <response code="400">Si los datos del producto son inválidos.</response>
        // POST: api/products
        [HttpPost]
        [Consumes("application/json")] // Solo acepta JSON en todo el controlador
        public ActionResult<Product> Post(Product product)
        {
            // Verificar el estado del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve un error 400 con los detalles de la validación
            }

            var products = _jsonFileService.ReadProducts();
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            _jsonFileService.SaveProducts(products);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">El ID del producto a actualizar.</param>
        /// <param name="product">Los nuevos datos del producto.</param>
        /// <returns>Una respuesta sin contenido.</returns>
        /// <response code="204">Si el producto se actualizó correctamente.</response>
        /// <response code="400">Si los datos del producto son inválidos.</response>
        /// <response code="404">Si el producto no existe.</response>
        // PUT: api/products/{id}
        [HttpPut("{id}")]
        [Consumes("application/json")] // Solo acepta JSON en todo el controlador
        public IActionResult Put(int id, Product product)
        {
            // Verificar el estado del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve un error 400 con los detalles de la validación
            }

            var products = _jsonFileService.ReadProducts();
            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            _jsonFileService.SaveProducts(products);

            return NoContent();
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">El ID del producto a eliminar.</param>
        /// <returns>Una respuesta sin contenido.</returns>
        /// <response code="204">Si el producto se eliminó correctamente.</response>
        /// <response code="404">Si el producto no existe.</response>
        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        [Consumes("application/json")] // Solo acepta JSON en todo el controlador
        public IActionResult Delete(int id)
        {
            var products = _jsonFileService.ReadProducts();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);
            _jsonFileService.SaveProducts(products);

            return NoContent();
        }
    }
}

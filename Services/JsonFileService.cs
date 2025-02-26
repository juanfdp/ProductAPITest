using System.Text.Json;
using ProductAPI.Models;
using System.IO;

namespace ProductAPI.Services
{
    public class JsonFileService
    {
        private readonly string _filePath = "products.json";

        // Leer productos desde el archivo JSON
        public List<Product> ReadProducts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Product>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        // Guardar productos en el archivo JSON
        public void SaveProducts(List<Product> products)
        {
            var json = JsonSerializer.Serialize(products);
            File.WriteAllText(_filePath, json);
        }
    }
}
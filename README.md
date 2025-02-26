# **ProductAPI**
## **Descripción del Proyecto**

ProductAPI es una Web API básica desarrollada en **ASP.NET Core** que permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) sobre un recurso de tipo **Product**. Los datos se persisten en un archivo JSON (`products.json`), lo que permite que la información se mantenga entre reinicios de la aplicación.

## **Funcionalidades**

- **Operaciones CRUD**:
  - **GET /api/products**: Obtener una lista de todos los productos.
  - **GET /api/products/{id}**: Obtener un producto por su ID.
  - **POST /api/products**: Crear un nuevo producto.
  - **PUT /api/products/{id}**: Actualizar un producto existente.
  - **DELETE /api/products/{id}**: Eliminar un producto por su ID.

- **Validaciones**:
  - El nombre del producto es obligatorio y debe tener entre 1 y 100 caracteres.
  - El precio y la cantidad deben ser mayores que 0.

- **Persistencia de Datos**:
  - Los productos se almacenan en un archivo JSON (`products.json`) en la raíz del proyecto.

- **Documentación**:
  - La API está documentada con **Swagger**, lo que permite probar los endpoints de manera interactiva.

## **Requisitos**

- **.NET SDK**: Versión 8.0.
- **Visual Studio 2022**
- **Postman** o cualquier herramienta para probar APIs (opcional).

## **Estructura del Proyecto**

- **Controllers**: Contiene los controladores de la API.
  - `ProductsController.cs`: Controlador para gestionar productos.
- **Models**: Contiene los modelos de datos.
  - `Product.cs`: Modelo que representa un producto.
- **Services**: Contiene los servicios de la aplicación.
  - `JsonFileService.cs`: Servicio para leer y escribir datos en un archivo JSON.
- **Program.cs**: Punto de entrada de la aplicación.
- **products.json**: Archivo JSON donde se almacenan los productos.

## **Ejemplos de Solicitudes y Respuestas**

### **Obtener todos los productos (GET /api/products)**

**Respuesta**:
```json
[
  {
    "id": 1,
    "name": "Producto 1",
    "price": 10.99,
    "quantity": 10
  }
]
```

### **Crear un Producto (POST /api/products)**

**Solicitud**:
```json
{
  "name": "Producto 1",
  "price": 10.99,
  "quantity": 10
}
```

**Respuesta**:
```json
{
  "id": 1,
  "name": "Producto 1",
  "price": 10.99,
  "quantity": 10
}
```

### **Obtener un Producto (GET /api/products/1)**

**Respuesta**:
```json
{
  "id": 1,
  "name": "Producto 1",
  "price": 10.99,
  "quantity": 10
}
```

### **Actualizar un Producto (PUT /api/products/1)**

**Solicitud**:
```json
{
  "name": "Producto Actualizado",
  "price": 15.99,
  "quantity": 20
}
```

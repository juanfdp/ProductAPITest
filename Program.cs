using Microsoft.OpenApi.Models;
using System.Reflection;
using ProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar el servicio JsonFileService
builder.Services.AddSingleton<JsonFileService>();

// Add services to the container.

builder.Services.AddControllers(options => {
        // Eliminar el formateador de salida de texto plano
        options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.StringOutputFormatter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener el nombre de las propiedades tal como están
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //Agrega la información de Swagger 
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductAPI",
        Version = "v1",
        Description = "PRUEBA TÉCNICA: DESARROLLO DE UNA WEB API EN C#/.NET ",
        Contact = new OpenApiContact
        {
            Name = "Juan Fernando Dorado Prado",
            Email = "juanfdp@gmail.com",
        }

    });

    // Incluir comentarios XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductAPI v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

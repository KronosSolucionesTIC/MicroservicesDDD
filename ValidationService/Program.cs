using Microsoft.EntityFrameworkCore;
using PaymentService.Infrastructure.Persistence;
using ValidationService.Infrastructure.Messaging;
using ValidationService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor de dependencias
builder.Services.AddSingleton<ValidationServiceImpl>();  // Corregido
builder.Services.AddSingleton<RabbitMQConsumer>();  // Corregido

// Configuración de Entity Framework Core con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Consumidor de RabbitMQ al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var consumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();

    // Iniciar consumo de mensajes en RabbitMQ cuando la aplicación arranque
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        Console.WriteLine("Starting RabbitMQ Consumer...");
        consumer.StartConsuming();
    });
}

app.Run();

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем службы к контейнеру
builder.Services.AddControllers();

// Добавляем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Reminder API",
        Description = "API для управления напоминаниями",
        Contact = new OpenApiContact
        {
            Name = "Ваше Имя",
            Email = "example@example.com"
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Настройка Swagger только для режима разработки
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reminder API v1");
        c.RoutePrefix = string.Empty; // Чтобы Swagger открывался по корневому URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

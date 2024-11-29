using Microsoft.OpenApi.Models;
using CVB.Models; // Подключение моделей, если они используются

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров
builder.Services.AddControllers();

// Добавляем CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Указываем ваш фронтенд
              .AllowAnyMethod()                     // Разрешаем любые HTTP-методы
              .AllowAnyHeader();                   // Разрешаем любые заголовки
    });
});

// Настраиваем Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Notification API",
        Description = "API для работы с уведомлениями",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "example@example.com",
            Url = new Uri("https://github.com/your-profile") // Укажите реальный URL, если нужно
        }
    });
});

var app = builder.Build();

// Применяем CORS
app.UseCors("AllowSpecificOrigins");

// Включаем Swagger для тестирования API
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification API v1");
        c.RoutePrefix = string.Empty; // Открываем Swagger по корневому URL
    });
}

// Middleware для HTTPS-редиректа
app.UseHttpsRedirection();

// Авторизация (если используется)
app.UseAuthorization();

// Подключение маршрутов контроллеров
app.MapControllers();

// Запуск приложения
app.Run();

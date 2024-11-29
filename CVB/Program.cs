using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку контроллеров
builder.Services.AddControllers();

// Добавляем CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Укажите адрес фронтенда
              .AllowAnyMethod()                     // Разрешаем любые HTTP-методы
              .AllowAnyHeader();                   // Разрешаем любые заголовки
    });
});

// Добавляем HttpClient для взаимодействия с внешними API
builder.Services.AddHttpClient();

// Добавляем Swagger для документации API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Integrated API",
        Description = "API для работы с уведомлениями и генерацией QR-кодов",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your-email@example.com",
            Url = new Uri("https://github.com/your-profile") // Укажите реальный URL
        }
    });
});

var app = builder.Build();

// Включаем CORS
app.UseCors("AllowSpecificOrigins");

// Включаем Swagger для разработки и продакшена
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integrated API v1");
        c.RoutePrefix = string.Empty; // Открываем Swagger по корневому URL
    });
}

// Редирект на HTTPS для безопасности
app.UseHttpsRedirection();

// Включаем авторизацию (если необходимо)
app.UseAuthorization();

// Подключаем маршруты контроллеров
app.MapControllers();

// Запускаем приложение
app.Run();

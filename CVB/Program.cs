var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Добавляем HttpClient для внешних запросов
builder.Services.AddHttpClient();

// Добавляем CORS для разрешения запросов с фронтенда
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Адрес фронтенда
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Настройка Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включаем CORS
app.UseCors("AllowSpecificOrigins");

// Включаем Swagger для отладки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Включаем маршрутизацию и контроллеры
app.UseAuthorization();
app.MapControllers();

// Запускаем приложение
app.Run();

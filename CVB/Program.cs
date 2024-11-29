using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� ��������� ������������
builder.Services.AddControllers();

// ��������� CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // ������� ����� ���������
              .AllowAnyMethod()                     // ��������� ����� HTTP-������
              .AllowAnyHeader();                   // ��������� ����� ���������
    });
});

// ��������� HttpClient ��� �������������� � �������� API
builder.Services.AddHttpClient();

// ��������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Integrated API",
        Description = "API ��� ������ � ������������� � ���������� QR-�����",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your-email@example.com",
            Url = new Uri("https://github.com/your-profile") // ������� �������� URL
        }
    });
});

var app = builder.Build();

// �������� CORS
app.UseCors("AllowSpecificOrigins");

// �������� Swagger ��� ���������� � ����������
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integrated API v1");
        c.RoutePrefix = string.Empty; // ��������� Swagger �� ��������� URL
    });
}

// �������� �� HTTPS ��� ������������
app.UseHttpsRedirection();

// �������� ����������� (���� ����������)
app.UseAuthorization();

// ���������� �������� ������������
app.MapControllers();

// ��������� ����������
app.Run();

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ � ����������
builder.Services.AddControllers();

// ��������� Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Reminder API",
        Description = "API ��� ���������� �������������",
        Contact = new OpenApiContact
        {
            Name = "���� ���",
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

// ��������� Swagger ������ ��� ������ ����������
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reminder API v1");
        c.RoutePrefix = string.Empty; // ����� Swagger ���������� �� ��������� URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

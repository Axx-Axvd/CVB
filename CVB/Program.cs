var builder = WebApplication.CreateBuilder(args);

// ��������� �����������
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// ��������� HttpClient ��� ������� ��������
builder.Services.AddHttpClient();

// ��������� CORS ��� ���������� �������� � ���������
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // ����� ���������
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ��������� Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// �������� CORS
app.UseCors("AllowSpecificOrigins");

// �������� Swagger ��� �������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// �������� ������������� � �����������
app.UseAuthorization();
app.MapControllers();

// ��������� ����������
app.Run();

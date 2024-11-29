using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/qr")]
public class QrApiController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public QrApiController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQrCode([FromBody] QrRequest request)
    {
        try
        {
            // Логируем входящие данные для отладки
            Console.WriteLine($"Field1: {request.Field1}, Field2: {request.Field2}, Field3: {request.Field3}");

            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(request.Field1) ||
                string.IsNullOrEmpty(request.Field2) ||
                string.IsNullOrEmpty(request.Field3))
            {
                return BadRequest("Все поля должны быть заполнены.");
            }

            string qrApiUrl = "http://helpful-orca-worthy.ngrok-free.app/api/GetQr";

            // Формируем запрос к API одногруппника
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(qrApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка от API одногруппника: {errorMessage}");
                return StatusCode((int)response.StatusCode, errorMessage);
            }

            // Получаем и возвращаем изображение
            var qrCode = await response.Content.ReadAsStreamAsync();
            return File(qrCode, "image/png");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Ошибка при запросе к API: {ex.Message}");
            return StatusCode(500, $"Ошибка при запросе к API: {ex.Message}");
        }
    }
}

// Модель для запроса
public class QrRequest
{
    public string Field1 { get; set; } // Текст для QR-кода
    public string Field2 { get; set; } // Цвет фона
    public string Field3 { get; set; } // Цвет кода
}

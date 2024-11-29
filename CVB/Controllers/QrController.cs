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
            // Проверка входных данных
            if (string.IsNullOrEmpty(request.InputData) ||
                string.IsNullOrEmpty(request.BgColor) ||
                string.IsNullOrEmpty(request.FgColor))
            {
                return BadRequest("Все поля должны быть заполнены.");
            }

            string qrApiUrl = "http://helpful-orca-worthy.ngrok-free.app/api/GetQr";

            // Формируем JSON-запрос
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Отправляем запрос к API одногруппника
            var response = await _httpClient.PostAsync(qrApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorMessage);
            }

            // Читаем и декодируем ответ
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var qrResponse = JsonSerializer.Deserialize<QrResponse>(jsonResponse);

            if (qrResponse == null || string.IsNullOrEmpty(qrResponse.OutputData))
            {
                return BadRequest("Ошибка при генерации QR-кода.");
            }

            // Возвращаем Base64-данные как изображение
            byte[] imageBytes = Convert.FromBase64String(qrResponse.OutputData);
            return File(imageBytes, "image/png");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ошибка: {ex.Message}");
        }
    }
}

// Модель для входного запроса
public class QrRequest
{
    public string InputData { get; set; } // Текст для QR-кода
    public string BgColor { get; set; }   // Цвет фона
    public string FgColor { get; set; }   // Цвет переднего плана
}

// Модель для ответа от API
public class QrResponse
{
    public string OutputData { get; set; } // Base64 строка с изображением
    public string Format { get; set; }    // Формат изображения
}

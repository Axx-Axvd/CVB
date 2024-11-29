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

    // POST: api/qr/generate
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQrCode([FromBody] QrRequest request)
    {
        try
        {
            // URL API одногруппника
            string qrApiUrl = "http://helpful-orca-worthy.ngrok-free.app/api/GetQr";

            // Создаем содержимое запроса
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Отправляем запрос к API
            var response = await _httpClient.PostAsync(qrApiUrl, content);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            // Возвращаем изображение QR-кода
            var qrCode = await response.Content.ReadAsStreamAsync();
            return File(qrCode, "image/png");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Ошибка при запросе к QR API: {ex.Message}");
        }
    }
}

// Модель для входящих данных
public class QrRequest
{
    public string Field1 { get; set; }
    public string Field2 { get; set; }
    public string Field3 { get; set; }
}

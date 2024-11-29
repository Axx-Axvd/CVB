using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public QrApiController()
        {
            _httpClient = new HttpClient();
        }

        // Прокси-метод для генерации QR-кода
        [HttpPost("proxy-generate-qr")]
        public async Task<IActionResult> ProxyGenerateQr([FromBody] QrRequest request)
        {
            var apiUrl = "http://helpful-orca-worthy.ngrok-free.app/api/GetQr";

            // Сериализуем запрос
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                InputData = request.InputData,
                BgColor = request.BgColor,
                FgColor = request.FgColor
            }), Encoding.UTF8, "application/json");

            try
            {
                // Отправляем запрос на API одногруппника
                var response = await _httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // Если внешний API возвращает ошибку, передаем ее обратно
                    return StatusCode((int)response.StatusCode, responseContent);
                }

                // Возвращаем успешный результат с содержимым ответа
                return Ok(responseContent);
            }
            catch (HttpRequestException ex)
            {
                // Обработка ошибок запроса
                return StatusCode(500, $"Ошибка при обращении к API: {ex.Message}");
            }
        }
    }

    // Класс для входящих данных запроса
    public class QrRequest
    {
        public string InputData { get; set; }
        public string BgColor { get; set; }
        public string FgColor { get; set; }
    }
}

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
    public async Task<IActionResult> GenerateQr([FromBody] QrRequest request)
    {
        var externalApiUrl = "http://helpful-orca-worthy.ngrok-free.app/api/GetQr";
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            InputData = request.InputData,
            BgColor = request.BgColor,
            FgColor = request.FgColor
        }), Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(externalApiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var qrImage = await response.Content.ReadAsByteArrayAsync();
            return File(qrImage, "image/png");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Ошибка при обращении к API: {ex.Message}");
        }
    }
}

public class QrRequest
{
    public string InputData { get; set; }
    public string BgColor { get; set; }
    public string FgColor { get; set; }
}

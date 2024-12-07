using CVB.Models;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ReminderController : ControllerBase
{
    private static List<Reminder> reminders = new List<Reminder>();
    private int _idCounter = 1; // Для генерации уникальных идентификаторов

    // POST: api/reminder/send
    [HttpPost("send")]
    public async Task<IActionResult> SendReminder([FromBody] Reminder reminder)
    {
        if (string.IsNullOrEmpty(reminder.Message) || string.IsNullOrEmpty(reminder.RecipientEmail))
        {
            return BadRequest("Message and RecipientEmail are required.");
        }

        try
        {
            // Генерация ID, времени и статуса на сервере
            reminder.Id = _idCounter++;
            reminder.ScheduledTime = DateTime.UtcNow; // Устанавливаем текущее время
            reminder.Status = "Pending"; // Начальный статус

            // Сохраняем напоминание в памяти
            reminders.Add(reminder);

            // Логика отправки почты
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("CVB Notifications", "your-email@example.com"));
            emailMessage.To.Add(new MailboxAddress("", reminder.RecipientEmail));
            emailMessage.Subject = "New Notification";
            emailMessage.Body = new TextPart("plain")
            {
                Text = reminder.Message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("your-email@example.com", "your-email-password");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            // Обновляем статус после успешной отправки
            reminder.Status = "Sent";

            return Ok(new
            {
                Status = reminder.Status,
                Message = "Email has been successfully sent to " + reminder.RecipientEmail
            });
        }
        catch (Exception ex)
        {
            reminder.Status = "Failed";
            return StatusCode(500, new
            {
                Status = reminder.Status,
                ErrorMessage = $"Error sending email: {ex.Message}"
            });
        }
    }
}

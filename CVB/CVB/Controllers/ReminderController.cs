using CVB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ReminderController : ControllerBase
{
    // Пример статической коллекции для хранения напоминаний
    private static List<Reminder> reminders = new List<Reminder>();

    // GET: api/reminder
    [HttpGet]
    public ActionResult<IEnumerable<Reminder>> GetAllReminders()
    {
        return Ok(reminders);
    }

    // GET: api/reminder/{id}
    [HttpGet("{id}")]
    public ActionResult<Reminder> GetReminder(int id)
    {
        var reminder = reminders.FirstOrDefault(r => r.Id == id);
        if (reminder == null)
            return NotFound("Напоминание с таким ID не найдено.");

        return Ok(reminder);
    }

    // POST: api/reminder
    [HttpPost]
    public ActionResult<Reminder> CreateReminder([FromBody] Reminder reminder)
    {
        // Установка уникального идентификатора для нового напоминания
        reminder.Id = reminders.Count > 0 ? reminders.Max(r => r.Id) + 1 : 1;
        reminders.Add(reminder);
        return CreatedAtAction(nameof(GetReminder), new { id = reminder.Id }, reminder);
    }

    // PUT: api/reminder/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateReminder(int id, [FromBody] Reminder updatedReminder)
    {
        var reminder = reminders.FirstOrDefault(r => r.Id == id);
        if (reminder == null)
            return NotFound("Напоминание с таким ID не найдено.");

        // Обновление данных напоминания
        reminder.Message = updatedReminder.Message;
        reminder.RecipientEmail = updatedReminder.RecipientEmail;
        reminder.ScheduledTime = updatedReminder.ScheduledTime;
        reminder.Status = updatedReminder.Status;

        return NoContent();
    }

    // DELETE: api/reminder/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteReminder(int id)
    {
        var reminder = reminders.FirstOrDefault(r => r.Id == id);
        if (reminder == null)
            return NotFound("Напоминание с таким ID не найдено.");

        reminders.Remove(reminder);
        return NoContent();
    }
}

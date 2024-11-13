namespace CVB.Models
{
    public class Reminder
    {
        public int Id { get; set; }                   // Уникальный идентификатор напоминания
        public string Message { get; set; }            // Текст сообщения напоминания
        public string RecipientEmail { get; set; }     // Email получателя
        public DateTime ScheduledTime { get; set; }    // Время, когда напоминание должно быть отправлено
        public string Status { get; set; }             // Статус напоминания (например, "Запланировано", "Отправлено", "Ошибка")

        // Дополнительно можно добавить вычисляемое свойство для удобства
        public bool IsScheduled => ScheduledTime > DateTime.Now; // Проверяет, запланировано ли напоминание
    }

}

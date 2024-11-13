namespace CVB.Models
{
    public class User
    {
        public int Id { get; set; }                 // Уникальный идентификатор пользователя
        public string Username { get; set; }        // Имя пользователя
        public string Email { get; set; }           // Email пользователя
        public string Password { get; set; }        // Хеш пароля пользователя (для безопасности используйте хеширование и не храните пароль в открытом виде)

        // Методы для работы с пользователями, такие как регистрация и авторизация, можно добавить в контроллер или сервис
    }

}

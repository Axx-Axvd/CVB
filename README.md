# CVB Project

Добро пожаловать в **Проект CVB** — многофункциональную платформу, объединяющую сервис уведомлений и генерацию QR-кодов в одном приложении. Этот проект демонстрирует, как можно сочетать современный функционал backend-API с интерактивным интерфейсом frontend, делая приложение универсальным и удобным в использовании
---

## Основные особенности

**Сервис уведомлений**
Легко отправляйте email-уведомления с помощью простой формы. Укажите адрес получателя, содержание сообщения и отправьте уведомление одним кликом.

**Генератор QR-кодов**
Генерируйте QR-коды, задавая необходимые данные и выбирая цвета фона и текста. Сгенерированные QR-коды мгновенно отображаются в интерфейсе.

**Бесшовная интеграция API**
Объединяет собственный API для уведомлений и внешний API для генерации QR-кодов, обеспечивая надёжность и масштабируемость.

**Удобный интерфейс**
Веб-интерфейс, созданный на базе React, отличается лаконичным дизайном, высокой отзывчивостью и простой навигацией.

---

## Технологии

- **Backend**
- ASP.NET Core 8.0 для создания RESTful API.
- C# для реализации бизнес-логики и интеграции.
- Внедрение зависимостей (Dependency Injection) для модульности и возможности тестирования.

**Frontend**
- React для создания динамичного и адаптивного интерфейса.
- Axios для выполнения HTTP-запросов.
- Bootstrap для стилизации и вёрстки.

**API**
- API для уведомлений: собственная разработка для отправки email-уведомлений.
- API для QR-кодов: внешний сервис, интегрированный через ngrok.

**Хостинг**
- Backend: размещён на Railway.app.
- API для QR-кодов: внешний сервис, подключён через ngrok.

---

## Установка

### Установка Бэка
1. Клонировать репозиторий:
   ```bash
   git clone git@github.com:Axx-Axvd/CVB.git
   cd CVB
2. Восстановите зависимости и соберите проект:
```
dotnet restore
dotnet build
```

## Использование
### Сервис уведомлений
1. Перейдите в раздел Notification Service в веб-интерфейсе.
2. Введите сообщение и email получателя.
3. Нажмите Send Notification, чтобы отправить письмо.

### Генератор QR-кодов
1. Перейдите в раздел QR Code Generator.
2. Укажите данные для QR-кода, а также желаемые цвета фона и текста.
3. Нажмите Generate QR Code, чтобы отобразить сгенерированный QR-код на странице.

## API Endpoints
### Notification API
**POST** /api/reminder
Sends an email notification.
Payload Example
```
{
  "message": "Test Notification",
  "recipientEmail": "example@example.com",
  "scheduledTime": "2024-11-15T10:00:00Z",
  "status": "Pending",
  "isScheduled": false
}
```
### QR Code API
**POST** /api/qr/generate
Generates a QR code.
**Payload Example**:
```
{
  "InputData": "Test Data",
  "BgColor": "#FFFFFF",
  "FgColor": "#000000"
}
```
# Вклад в проект
Мы будем рады вашим предложениям и улучшениям!

1. Сделайте форк репозитория.
2. Создайте новую ветку для вашей доработки или исправления ошибки.
3. Отправьте pull request с подробным описанием внесённых изменений.

# Лицензия
Этот проект распространяется по лицензии MIT.
Вы можете свободно использовать, модифицировать и распространять его согласно условиям лицензии.

# Благодарности
- Railway.app за хостинг backend-части проекта.
- Сообществу React за создание отличного фреймворка для frontend.
Приступайте к изучению проекта CVB и оцените удобный способ управления уведомлениями и генерации QR-кодов! 🚀

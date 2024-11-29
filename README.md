# CVB Project

Welcome to the **CVB Project**, a comprehensive platform designed to integrate notification services and QR code generation into a single streamlined application. This project showcases the power of combining modern backend API functionality with an interactive frontend interface, making it versatile and user-friendly.

---

## Features

- **Notification Service**  
  Easily send email notifications through a straightforward interface. Enter the recipient's email, message details, and send notifications in just one click.

- **QR Code Generator**  
  Generate custom QR codes by inputting data and selecting foreground and background colors. The generated QR codes are displayed instantly on the interface.

- **Seamless API Integration**  
  Combines custom-built notification API with external QR code generation API for a robust and scalable solution.

- **User-Friendly Interface**  
  Built with React, the web interface is clean, intuitive, and responsive, ensuring smooth user interactions.

---

## Technologies Used

- **Backend**  
  - ASP.NET Core 8.0 for building RESTful APIs.
  - C# for business logic and integration.
  - Dependency injection for modular, testable code.

- **Frontend**  
  - React for building a dynamic and responsive UI.
  - Axios for handling HTTP requests.
  - Bootstrap for styling and layout.

- **APIs**  
  - Notification API: Custom-developed to send email notifications.
  - QR Code API: External API integration for generating QR codes dynamically.

- **Hosting**  
  - Backend: Hosted on [Railway.app](https://railway.app).
  - QR Code API: External service integrated via ngrok.

---

## Installation

### Backend Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/CVB.git
   cd CVB
2. Restore dependencies and build the project:
```
dotnet restore
dotnet build
```
## FrontEnd Setup
1. Navigate to the frontend directory
```
cd CVB-Frontend
```
2. Install dependencies
```
npm install
```
3. Start the development server
```
npm start
```
4. Open your browser and visit http://localhost:3000

## Usage
### Notification Service
Navigate to the Notification Service section on the web interface.
Enter the message and the recipient's email address.
Click Send Notification to dispatch the email.
### QR Code Generator
Navigate to the QR Code Generator section.
Enter the data for the QR code along with your desired background and foreground colors.
Click Generate QR Code to display the generated QR code on the page.

## API Endpoints
Notification API
**POST** /api/reminder
Sends an email notification.
Payload Example
```
{
  "message": "Hello, this is a test notification!",
  "recipientEmail": "example@example.com"
}
```
QR Code API
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
# Contributing
We welcome contributions to make this project even better!

1. Fork the repository.
2. Create a new branch for your feature/bugfix.
3. Submit a pull request with a detailed explanation of your changes.

# License
This project is licensed under the MIT License.

Feel free to use, modify, and distribute this project as per the license terms.

# Acknowledgements
Railway.app for hosting the backend.
The React community for building such a fantastic frontend framework.

Start exploring the CVB Project now and experience a seamless way to manage notifications and QR codes! 🚀

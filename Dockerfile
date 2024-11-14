# Используем .NET SDK 8.0 для этапа сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Копируем CSPROJ и восстанавливаем зависимости
COPY CVB/*.csproj ./CVB/
RUN dotnet restore ./CVB/CVB.csproj

# Копируем остальные файлы и собираем приложение
COPY . ./
WORKDIR /app/CVB
RUN dotnet publish -c Release -o out

# Используем Runtime для этапа запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/CVB/out .

# Указываем порт и команду для запуска
EXPOSE 80
ENTRYPOINT ["dotnet", "CVB.dll"]

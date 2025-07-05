
# 🌤️ Weather API Wrapper Service

A .NET Web API that wraps the Visual Crossing Weather API, providing authenticated access to weather data with Redis caching and PostgreSQL logging.

---

<img width="861" height="624" alt="Image" src="https://github.com/user-attachments/assets/3f9ccc23-6f3f-4514-96d2-5516700fe43f" />

## 🚀 Features

- 🔐 JWT Authentication and Role-based Authorization
- 📦 Weather Data Wrapper (for Visual Crossing API)
- 💾 Redis Caching for API responses
- 📝 PostgreSQL Logging for user requests
- 🔍 Swagger UI for testing and documentation
- 🧼 Clean, extensible architecture

---

## 📁 Project Structure

```
WeatherApiWrapper/
│
├── Controllers/
│   └── WeatherController.cs
│
│ 
├── Repositories/
│   ├── APIWeatherRepository.cs
│   └── DbWeatherRepository.cs
│
├── Models/
│   ├── WeatherLog.cs
├───DTOs/
│   └── WeatherRequestDto.cs
│   └── RegisterRequestDto.cs
│   └── LoginRequestDto.cs
│
├── Interfaces/
│   ├── IWeatherApiService.cs
├───Caching/
│    └── ICacheService.cs
│    └── RedisCacheService.cs
│
├── Data/
│   ├── AppDbContext.cs
│   └── IdentityDbContext.cs
│ 
├── Helpers/
│   ├── JWTHelper.cs
│   └── ApiClient.cs
│
├── docker-compose.yml
├── Program.cs
├── appsettings.json
└── README.md
```

---

## ⚙️ Tech Stack

- [.NET 8](https://dotnet.microsoft.com/)
- [Redis](https://redis.io/)
- [PostgreSQL](https://www.postgresql.org/)
- [Visual Crossing API](https://www.visualcrossing.com/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Swagger / Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

---

## 🐳 Docker Setup (Redis)

Ensure you have Docker installed, then run:

```bash
docker compose up -d
```

**docker-compose.yml**
```yaml
version: '3.8'

services:
  redis:
    image: redis:alpine
    container_name: redis
    ports:
      - "6379:6379"
```

---

## 🔑 Authentication

- JWT-based login required for accessing the weather endpoint
- Tokens include `sub` claim with User ID
- Only authenticated users can request data

---

## 📦 Usage

### 1. Clone the repository
```bash
git clone https://github.com/your-username/WeatherApiWrapper.git
cd WeatherApiWrapper
```

### 2. Setup configuration
Update `appsettings.json` with:
- Redis connection string
- PostgreSQL connection string
- JWT secret and issuer
- Visual Crossing API key

### 3. Run EF Migrations
```bash
dotnet ef database update
```

### 4. Run the application
```bash
dotnet run
```

### 5. Access Swagger
Go to: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 📥 API Usage

### `POST /api/weather`
Fetch weather data by location and date range.
```json
{
  "location": "Cairo",
  "startDate": "2025-07-01",
  "endDate": "2025-07-04"
}
```

- Checks Redis cache first
- Calls external API if not found
- Logs request to PostgreSQL
- Returns `ResponseSource = CACHE` or `API`

---

## ✅ Todos
- [ ] Add unit and integration tests
- [ ] Add refresh tokens
- [ ] Add rate limiting

---

## 📄 License

MIT License

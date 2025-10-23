# 🔗 URL Shortener - Minimal API (.NET 8)

A lightweight, testable, and extensible **URL Shortener** built using **ASP.NET Core .NET 8 Minimal API**.  
It provides a production-ready implementation for shortening and redirecting URLs, with full **integration testing** support using **xUnit** and **FluentAssertions**.

---

## 🚀 Features

- 🔹 **Shorten URLs** – Convert long URLs into compact short links  
- 🔹 **Automatic Redirection** – Redirects to the original URL using HTTP 302  
- 🔹 **Case-Insensitive & Slash-Tolerant** – Works with or without trailing `/`  
- 🔹 **Minimal API** – Clean and efficient endpoint definitions  
- 🔹 **In-Memory Database** – Fast, isolated, and test-friendly data persistence  
- 🔹 **Test Coverage** – Full integration tests included  
- 🔹 **Dependency Injection Ready** – Uses interfaces for maintainable and testable design  

---

## 🧩 Tech Stack

| Layer | Technology |
|-------|-------------|
| **Framework** | .NET 8 (ASP.NET Core Minimal API) |
| **Database** | InMemory Database (via [EF Core](https://learn.microsoft.com/en-us/ef/core/)) |
| **ORM** | [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) |
| **Testing** | [xUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/) |
| **Dependency Injection** | Built-in .NET DI |
| **Build Tooling** | dotnet CLI |

---

## 📁 Project Structure

```
UrlShortner-Minimal-API/
│
├── src/
│   ├── UrlShortner.Api/                # Minimal API endpoints
│   ├── UrlShortner.Data/               # EF Core DbContext & entities
│   ├── UrlShortner.Interfaces/         # Contracts & abstractions
│   ├── UrlShortner.Services/           # Business logic & implementations
│
├── tests/
│   ├── UrlShortner.IntegrationTests/   # xUnit + FluentAssertions integration tests
└── README.md
```

---

## ⚙️ Getting Started

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/samanbahrekazemi/UrlShortner-Minimal-API.git
cd UrlShortner-Minimal-API
```

### 2️⃣ Run the API
```bash
dotnet run --project src/UrlShortner.Api
```

The API will run on:  
👉 `https://localhost:5001` or `http://localhost:5000`

---

## 🧠 API Endpoints

### 🔹 Shorten a URL
**POST** `/api/shortner`

```json
{
  "originalUrl": "https://google.com"
}
```

**Response**
```json
{
  "shortUrl": "https://localhost:5001/api/shortner/abc123"
}
```

---

### 🔹 Redirect to Original
**GET** `/api/shortner/{key}`  

Redirects to the original URL using **HTTP 302 Found**

---

### 🔹 Get All Shortened URLs
**GET** `/api/shortner/all`  

Returns a JSON array of all shortened URLs:

```json
[
  {
    "shortUrlKey": "abc123",
    "originalUrl": "https://google.com",
    "createdAt": "2025-10-23T12:00:00Z",
    "modifiedAt": "2025-10-23T12:00:00Z"
  }
]
```

---

## 🧪 Running Tests

Full integration tests included using **xUnit** + **FluentAssertions**.

### Run Tests
```bash
dotnet test
```

### Example Tested Scenarios
- ✅ `POST /api/shortner` returns a valid short URL  
- ✅ `GET /api/shortner/{key}` redirects to original URL  
- ✅ Handles both `https://google.com` and `https://google.com/` correctly  
- ✅ Returns `404` for invalid short URLs  
- ✅ `GET /api/shortner/all` returns all saved mappings  

---

## 🧰 Dependencies

| Package | Description |
|---------|-------------|
| `Microsoft.EntityFrameworkCore.InMemory` | In-memory EF Core database for development/testing |
| `FluentAssertions` | Human-readable test assertions |
| `xUnit` | Unit & integration testing framework |
| `Microsoft.AspNetCore.Mvc.Testing` | Simplifies end-to-end API integration testing |

---

## 🧑‍💻 Author

**Saman Bahrekazemi**  
GitHub: [@samanbahrekazemi](https://github.com/samanbahrekazemi)

---

## 🪪 License

This project is licensed under the **MIT License**.

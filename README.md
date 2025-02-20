# URL Shortener - Minimal API (v8)

This is a URL shortener built with **.NET 8 Minimal API**. The project allows users to shorten long URLs and redirect them to the original long URL upon accessing the short version.

---

## Features

- **Shorten URLs**: Converts long URLs into shortened ones.
- **Custom Aliases**: Optionally specify custom aliases for shortened URLs.
- **Redirection**: Automatically redirects users to the original URL.
- **Minimal API**: Simple and clean implementation using .NET 8 Minimal API.
- **Persistent Storage**: URLs are stored in a database for persistence (SQLite by default).

---

## Requirements

- **.NET 8 SDK** or higher
- A relational database (SQLite or SQL Server recommended)
- A text editor or IDE (Visual Studio, VS Code, etc.)

---

## Setup

### 1. Clone the Repository

Start by cloning the repository to your local machine:

```bash
git clone https://github.com/samanbahrekazemi/UrlShortner-Minimal-API.git
cd UrlShortner-Minimal-API

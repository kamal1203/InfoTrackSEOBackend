# InfoTrack SEO Rank Scraper Backend

This project contains the backend API for the InfoTrack SEO Rank Scraper application. It allows users to track the ranking of a specific URL for given keywords on Google search results.

## Features

*   Scrapes Google search results (top 100) for specified keywords and target URL.
*   Uses custom scraping logic without relying on third-party HTML parsing libraries.
*   Supports configurable scraping strategies (currently HTTP GET, Selenium planned).
*   Stores historical search ranking data in a SQL Server database using Entity Framework Core.
*   Provides API endpoints to trigger searches and retrieve historical ranking data.
*   Includes unit and integration tests.

## Prerequisites

*   .NET 8 SDK (or later)
*   SQL Server (including SQL Express or LocalDB) for database storage.
*   (Optional) ChromeDriver installed and accessible via PATH if using the Selenium scraping strategy (not default).

## Setup

1.  **Clone the repository:**
    ```bash
    git clone <repository-url>
    cd InfoTrackSEO
    ```

2.  **Configure Application Settings:**
    *   Open the `InfoTrackSEO.API/appsettings.json` (and/or `appsettings.Development.json`) file.
    *   Update the following configuration sections as needed:
        - **ConnectionStrings:DefaultConnection**: Set your SQL Server instance and database name.

        **OPTIONAL**
        - **Scraping**:
        - The scraping methods other than powershell are either not working or really unstable, powershell is recommended
            - `DefaultStrategy`: The scraping strategy to use (e.g., `HttpManualParseStrategy`, `SeleniumManualParseStrategy`).
            - `UserAgent`: The user agent string for scraping requests.
            - `ChromeUserDataDir`: The Chrome user data directory for Selenium scraping.
        - **Cors:AllowedOrigins**: List of allowed origins for CORS (set to `*` for all origins).
    *   Example:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=.\\SQLEXPRESS;Database=InfoTrackSeoDb;Trusted_Connection=True;TrustServerCertificate=True;"
        },
        "Scraping": {
          "DefaultStrategy": "HttpManualParseStrategy",
          "UserAgent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) ...",
          "ChromeUserDataDir": "c:/Users/Kamal/source/repos/InfoTrackSEO/.selenium_profiles/chrome"
        },
        "Cors": {
          "AllowedOrigins": ["*"]
        }
        ```

4.  **Apply Database Migrations:**
    *   Open a terminal or command prompt in the solution root directory (`InfoTrackSEO`).
    *   Run the following command to apply the initial database schema:
    ```bash
    dotnet ef database update --project InfoTrackSEO.Data --startup-project InfoTrackSEO.API
    ```
    *   This will create the `InfoTrackSeoDb` database (or whatever you named it in `appsettings.json`) and the necessary tables based on the latest migration.

## Running the Application

1.  **Build the solution:**
    ```bash
    dotnet build InfoTrackSEO.sln
    ```
2.  **Run the API:**
    ```bash
    dotnet run --project InfoTrackSEO.API
    ```
3.  The API will start, typically listening on `https://localhost:xxxx` and `http://localhost:yyyy` (check the console output for the exact ports).
4.  Make a note of this port as you will need to add it to the front end config file


## Project Structure

*   **InfoTrackSEO.Core:** Contains core business logic, domain models, and interfaces.
*   **InfoTrackSEO.Data:** Handles data access using EF Core and SQL Server. Contains DbContext, Migrations, and Repository implementations.
*   **InfoTrackSEO.API:** ASP.NET Core Web API project providing HTTP endpoints. Contains Controllers, DI setup, and configuration.
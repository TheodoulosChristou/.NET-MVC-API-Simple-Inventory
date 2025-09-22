# .NET MVC + API – Simple Inventory

A small inventory management application built with **ASP.NET Core MVC** and an integrated **REST API**.

The project demonstrates:
* MVC views for a lightweight web UI.
* A documented REST API with **OpenAPI**.
* **Entity Framework Core** (SQLite) for data access.
* **FluentValidation** for input validation.
* Service-layer business rules (duplicate SKU detection, category deletion checks).

---

## ✨ Features
* Manage **Products** (create, update, search/filter, delete).
* Manage **Categories** (create, delete – protected if products exist).
* API endpoints documented via **OpenAPI JSON** and **Scalar** interactive docs.

---

## 🚀 Getting Started

### Prerequisites
* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download) (or the SDK targeted by the project)
* Optional: `dotnet-ef` CLI if you plan to run migrations  
  ```bash
  dotnet tool install -g dotnet-ef

### Clone & Restore
```bash
git clone https://github.com/TheodoulosChristou/.NET-MVC-API-Simple-Inventory.git
cd .NET-MVC-API-Simple-Inventory
dotnet restore

### Clone & Restore
The app uses SQLite with a connection string in appsettings.json.
First run will create the DB file automatically.
If you add EF migrations:
```bash
dotnet ef database update --project SimpleInventory

### Run
```bash
dotnet run --project SimpleInventory

The app will start at something like:

https://localhost:7110 (HTTPS)
https://localhost:7110/scalar/ (HTTP) (you can find documentation in scalar)


### Design Decisions & Trade-offs

SQLite for dev/demo – portable and zero-config.
Trade-off: limited for high-concurrency production; swap for Postgres or SQL Server if needed.

Service Layer – business rules (duplicate SKU, category deletion rule) live in services, not controllers, making them testable and reusable.

Validation – FluentValidation
 enforces clean rules (price ≥ 0, name/sku required) instead of scattered checks.

OpenAPI first – auto-generated spec used by both Scalar (modern docs) and Swagger UI for a great developer experience.

Paging & Filtering – endpoints accept page and pageSize plus filters (name, sku, categoryId) to avoid over-fetching.




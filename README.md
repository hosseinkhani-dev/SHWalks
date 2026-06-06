SHWalks — Decoupled Full-Stack Architecture Exploration

A modern, full-stack web application designed to track, explore, and manage regional walking trails and geographical areas.
This project was built as a dedicated architectural exploration to master decoupled system communication and secure token handshakes.

---

## 🏗️ System Architecture

This system is completely decoupled into two independent application layers to mirror modern, cloud-native enterprise deployment patterns:

### 1. Presentation Layer (ASP.NET Core MVC)

* Manages client-side state, user interaction workflows, and layout rendering.
* Utilizes `IHttpClientFactory` to communicate asynchronously with the backend over a secure network.
* Handles state tracking natively via encrypted, time-locked **HTTP-Only Cookies**.

### 2. Core Backend Engine (Web API)

* Built using **Clean Architecture** principles to separate domain models, business logic, and infrastructure layers.
* Features a robust database persistence layer managed by **Entity Framework Core** using the Repository pattern.
* Implements **ASP.NET Core Identity** and cryptographically signs state records using standard **JSON Web Tokens (JWT)**.

---

## ⚡ Modern Engineering: AI-Assisted Development Flow

To maximize feature delivery speed and mirror real-world engineering environments, this project embraces modern development practices by treating **Artificial Intelligence as a productivity multiplier**:

* **Backend Engineering (100% Handwritten):** The underlying architecture—including Clean Architecture boundaries, custom JWT generation, API endpoints, repository filters, state transitions, and database relational seed matrices—was fully architected and written from scratch.
* **Frontend Velocity (AI-Accelerated):** To bypass slow styling phases, AI tools were leveraged to rapidly generate, prototype, and polish the responsive `.cshtml` Razor pages and layout matrix configurations using **Bootstrap 5**.
* **The Takeaway:** This approach showcases my ability to orchestrate, debug, and safely integrate AI outputs into an enterprise-ready C# full-stack environment.

---

## 🛠️ Tech Stack & Key Features

* **Core Engine:** .NET 8 C# ASP.NET Core
* **Data Access:** Entity Framework Core (SQL Server)
* **Security Pipeline:** JWT Authentication Bearer Tokens
* **UI Framework:** Razor Views, Bootstrap 5

---

## 💾 Database State Initialization & Verification

The database includes robust seed data to instantly provide a rich testing experience for evaluators.

### Testing Authentication Matrix

To test permission-restricted actions (such as adding, updating, or deleting walking trails), you can create an account using the UI. New registrations automatically grant a **Writer** role identity claim to your cookie session.

---

## 🚀 Local Installation & Setup

1. **Clone the repository:**
```bash
git clone https://github.com/hosseinkhani-dev/SHWalks.git
cd SHWalks

```


2. **Initialize Backend Database Migrations:**
Navigate to the root solution directory and run the Entity Framework command specifying both layers:
```bash
dotnet ef database update --project SHWalks.Infrastructure --startup-project SHWalks.API

```


3. **Run the projects simultaneously:**
Configure your IDE to launch both `SHWalks.API` and `SHWalks.UI` at the same time, or use the terminal from their respective directories:
```bash
dotnet run

```



```

```

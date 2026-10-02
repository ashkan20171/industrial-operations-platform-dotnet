# AIOP — Ashkan Industrial Operations Platform

> A portfolio-grade **.NET 8 / WPF industrial operations platform** that brings asset health, alarms, maintenance, inventory, telemetry, auditability and edge-integration concepts into one maintainable desktop application.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/UI-WPF-0C54C2)
![Architecture](https://img.shields.io/badge/Architecture-MVVM%20%2B%20Services-22C55E)
![Industrial](https://img.shields.io/badge/Industrial-OPC%20UA%20%7C%20MQTT-F59E0B)
![License](https://img.shields.io/badge/License-Portfolio-lightgrey)

## Why this project exists

Industrial desktop software is often judged on much more than CRUD screens. It must make abnormal conditions visible, keep operator workflows understandable, preserve traceability, respect permissions, and remain maintainable as integrations grow.

AIOP is my engineering portfolio project for that problem space. It demonstrates how I structure a modern C# desktop application around **operations, reliability, security boundaries, persistence and human-centred UI/UX**. It intentionally distinguishes implemented demo behaviour from production integrations rather than claiming simulated data is connected to real PLCs.

## Product tour

| Area | What it demonstrates |
| --- | --- |
| **Command Center** | OEE, asset health, active alarms, maintenance workload and explainable operational risk |
| **Asset Management** | Equipment registry, status and condition indicators |
| **Alarm Center** | Severity/state workflow and operational acknowledgement concepts |
| **Maintenance** | Work orders, priorities, ownership and preventive scheduling |
| **Live Telemetry** | Streaming-style machine condition visualization using a simulator abstraction |
| **Inventory** | Spare-parts stock and reorder-point awareness |
| **Reliability Insights** | Transparent risk scoring with evidence and recommended actions |
| **Operational Health** | Cross-domain findings across assets, alarms, maintenance and stock |
| **Industrial Connectivity** | Adapter-oriented OPC UA / MQTT / REST architecture and connection state |
| **Users & Audit** | Role-based access concepts and operational traceability |
| **Engineering Overview** | In-app architecture map, engineering principles and production roadmap |

## Architecture

```text
┌──────────────────────────── WPF Views ────────────────────────────┐
│  Command Center │ Assets │ Alarms │ Maintenance │ Edge │ Admin   │
└──────────────────────────────┬────────────────────────────────────┘
                               │ Data binding / incremental MVVM
┌──────────────────────────────▼────────────────────────────────────┐
│ ViewModels / Presentation                                         │
│ ObservableObject • RelayCommand • DashboardViewModel              │
└──────────────────────────────┬────────────────────────────────────┘
                               │
┌──────────────────────────────▼────────────────────────────────────┐
│ Application Services                                              │
│ Auth • Session • Permissions • Operational Summary • Health       │
│ Reliability • Connectivity • Telemetry • Persistence              │
└──────────────────────────────┬────────────────────────────────────┘
                               │ contracts
┌──────────────────────────────▼────────────────────────────────────┐
│ Infrastructure                                                    │
│ IPlantDataStore • IUserStore • IAuditStore                        │
│ Atomic/versioned JSON implementations                             │
└───────────────────────────────────────────────────────────────────┘
```

The MVVM migration is deliberately incremental. Moving one bounded area at a time keeps regression risk lower than a portfolio-driven “big bang” rewrite and leaves the architecture easy to discuss in a code review.

## Engineering highlights

- **.NET 8 WPF** desktop application with a dark industrial command-centre visual language.
- **Incremental MVVM** foundation (`ObservableObject`, `RelayCommand`, ViewModels) with business calculations extracted from code-behind.
- **Role-based navigation** for Administrator, Supervisor, Maintenance Engineer, Operator and Viewer roles.
- **Repository abstraction** through `IPlantDataStore`; UI/domain code is not coupled to the JSON storage implementation.
- **Atomic local persistence** for assets, alarms, work orders, inventory and maintenance plans under `%LocalAppData%`.
- **Explainable reliability scoring**: evidence and recommended actions are shown instead of presenting heuristics as machine learning.
- **Industrial integration boundary** prepared for OPC UA, MQTT and REST adapters without pretending demo endpoints are live plant connections.
- **Operational audit trail**, session handling and password hashing abstractions.
- **Permission-aware modules**, global search and recruiter-friendly Engineering Overview inside the application.

## UX approach

The interface uses a restrained dark navy base to reduce glare in monitoring contexts, cyan for primary interaction/technical focus, green for healthy states, amber for attention and rose/red for critical states. Colour is paired with labels and status text so meaning is not conveyed by colour alone.

The UI prioritises **exception visibility, information hierarchy and fast scanning** over decorative dashboard density.

## Run locally

### Requirements

- Windows 10/11
- Visual Studio 2022 with **.NET desktop development** workload, or .NET 8 SDK

### Visual Studio

1. Clone the repository.
2. Open `AshkanIndustrialOperations.sln` if present, or `AshkanIndustrialOperations.csproj`.
3. Restore/build the project.
4. Run with `F5`.

### CLI

```powershell
dotnet restore
dotnet build -c Release
dotnet run --project .\AshkanIndustrialOperations.csproj
```

### Demo account

```text
Username: admin
Password: 1234
```

> The credentials above are **demo-only**. A production deployment should use an identity provider or a hardened credential lifecycle and secret-management strategy.

## Persistence

Operational demo data is stored through `IPlantDataStore`. The current implementation writes a versioned snapshot atomically to:

```text
%LocalAppData%\AshkanIndustrialOperations\plant-data.v1.json
```

This boundary is intentionally designed so a future `EfCorePlantDataStore` can target SQL Server or SQLite without rewriting the WPF views.

## Industrial connectivity: current scope

The connectivity module models the lifecycle and architecture required for industrial protocols. It is **adapter-ready**, not a claim of a certified live PLC connection.

Production integration would add tested client libraries, certificate trust management, secure credential storage, reconnect/backoff policies, namespace/tag mapping, quality/timestamp handling and integration tests against representative devices or simulators.

## Roadmap

- [x] Asset / alarm / maintenance / inventory operational modules
- [x] Role-based navigation and session-aware shell
- [x] Auditability and atomic local persistence
- [x] Reliability and operational-health analysis
- [x] OPC UA / MQTT / REST adapter boundary
- [x] Incremental MVVM foundation
- [ ] Complete ViewModel migration for all modules
- [ ] EF Core implementation with SQLite + SQL Server provider option
- [ ] Automated unit/integration tests and CI pipeline
- [ ] Structured logging + OpenTelemetry instrumentation
- [ ] Real OPC UA / MQTT client adapters with secure configuration
- [ ] Signed installer / release packaging
- [ ] ML.NET anomaly model only after obtaining representative labelled telemetry data

## What I would discuss in an interview

This project gives me concrete trade-offs to discuss: why I chose incremental MVVM instead of a risky rewrite; why persistence is behind an interface; how I would secure OT/IT boundaries; why alarm UX must prioritise exceptions; how I separate heuristic risk scoring from actual ML; and how the desktop client could evolve toward SQL-backed and edge-connected production architecture.

## Repository structure

```text
AshkanIndustrialOperations/
├── Infrastructure/   # Persistence and audit/user-store contracts + implementations
├── Models/           # Operational domain/data models
├── Security/         # Password hashing concerns
├── Services/         # Application, reliability, connectivity and persistence services
├── ViewModels/       # Incremental MVVM presentation layer
└── Views/            # WPF user interface
```

## Author

**Ashkan Motaei**  
Software Engineer / C# / .NET

If you are reviewing this repository for a software engineering role, I recommend starting with `DashboardViewModel`, `OperationalSummaryService`, `PermissionService`, `IPlantDataStore`, `JsonPlantDataStore`, `OperationalHealthService` and `IndustrialConnectivityService`.
"# industrial-operations-platform-dotnet" 

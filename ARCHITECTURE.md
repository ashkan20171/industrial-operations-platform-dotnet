# Architecture Notes

## Direction
AIOP is moving from a pragmatic WPF code-behind application toward MVVM through bounded, incremental refactoring. The goal is testability and separation of concerns without destabilising working modules.

## Boundaries
- **Views** render and collect user interaction.
- **ViewModels** expose presentation state and commands.
- **Services** own application/business calculations and integration orchestration.
- **Infrastructure** owns persistence mechanisms behind interfaces.
- **Models** describe operational state.

## Persistence decision
The current JSON implementation is dependency-free and atomic. `IPlantDataStore` is the seam for an EF Core implementation. Production would add transactions, concurrency control, migrations, database health checks and backup strategy.

## OT connectivity decision
OPC UA/MQTT are represented as adapters and connection states. Production implementation must include certificate lifecycle, encrypted secret storage, reconnect policy, telemetry quality/timestamps, tag mapping, rate limiting and realistic integration testing.

## Reliability decision
Current risk scores are deterministic and explainable. They are not called AI/ML. A trained model belongs here only after representative data, labels, validation metrics and drift monitoring exist.

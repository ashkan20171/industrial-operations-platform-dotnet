using System.Collections.ObjectModel;
using AshkanIndustrialOperations.Models;
namespace AshkanIndustrialOperations.Services;
public sealed class IndustrialConnectivityService
{
    public ObservableCollection<IndustrialConnection> Connections { get; } = new()
    {
        new(){Name="Packaging Line OPC UA",Protocol="OPC UA",Endpoint="opc.tcp://edge-gateway:4840",Security="Basic256Sha256 • Sign & Encrypt",Status="Configured"},
        new(){Name="Plant MQTT Broker",Protocol="MQTT",Endpoint="mqtts://broker.local:8883",Security="TLS 1.2+ • Credentials",Status="Configured"},
        new(){Name="Historian Bridge",Protocol="REST/HTTPS",Endpoint="https://historian.local/api/v1",Security="TLS • Token",Status="Disabled"}
    };
    public string ArchitectureSummary => "Adapters → validation → normalized telemetry → rules/health → UI & persistence";
}

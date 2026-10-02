namespace AshkanIndustrialOperations.Models;
public sealed class IndustrialConnection
{
    public string Name { get; set; } = "";
    public string Protocol { get; set; } = "OPC UA";
    public string Endpoint { get; set; } = "";
    public string Security { get; set; } = "Sign & Encrypt";
    public string Status { get; set; } = "Configured";
    public string LastSeen { get; set; } = "Not connected";
}

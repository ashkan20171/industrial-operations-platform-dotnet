namespace AshkanIndustrialOperations.Models;
public sealed class TelemetrySample { public DateTime Timestamp {get;set;}=DateTime.Now; public string Asset {get;set;}=""; public double Temperature {get;set;} public double Vibration {get;set;} public double Pressure {get;set;} public double PowerKw {get;set;} }

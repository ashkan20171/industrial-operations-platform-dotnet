using System.Collections.ObjectModel;
using AshkanIndustrialOperations.Models;
using AshkanIndustrialOperations.Services;
namespace AshkanIndustrialOperations.ViewModels;
public sealed class DashboardViewModel : ObservableObject
{
    private readonly DemoDataService _data; private readonly Stage6DataService _stage6; private readonly OperationalSummaryService _summary;
    private string _oee="0%",_health="0 / 0",_attention="",_alarms="0",_critical="",_maintenance="0",_nextMaintenance="",_risk="0 / 100",_riskLabel="";
    public DashboardViewModel(DemoDataService data, Stage6DataService stage6, OperationalSummaryService summary){_data=data;_stage6=stage6;_summary=summary;Refresh();}
    public ObservableCollection<Machine> Machines=>_data.Machines;
    public string Oee {get=>_oee;private set=>SetProperty(ref _oee,value);} public string Health {get=>_health;private set=>SetProperty(ref _health,value);}
    public string Attention {get=>_attention;private set=>SetProperty(ref _attention,value);} public string Alarms {get=>_alarms;private set=>SetProperty(ref _alarms,value);}
    public string Critical {get=>_critical;private set=>SetProperty(ref _critical,value);} public string Maintenance {get=>_maintenance;private set=>SetProperty(ref _maintenance,value);}
    public string NextMaintenance {get=>_nextMaintenance;private set=>SetProperty(ref _nextMaintenance,value);} public string Risk {get=>_risk;private set=>SetProperty(ref _risk,value);}
    public string RiskLabel {get=>_riskLabel;private set=>SetProperty(ref _riskLabel,value);}
    public void Refresh(){var s=_summary.Build(_data,_stage6);Oee=$"{s.AverageOee:F1}%";Health=$"{s.HealthyAssets} / {s.TotalAssets}";Attention=$"{s.AttentionAssets} require attention";Alarms=s.ActiveAlarms.ToString();Critical=$"{s.CriticalAlarms} critical";Maintenance=s.OpenWorkOrders.ToString();Risk=$"{s.RiskScore} / 100";RiskLabel=s.RiskLabel;var next=_stage6.Plans.OrderBy(x=>x.Due).FirstOrDefault();NextMaintenance=next is null?"No scheduled tasks":$"{next.Asset} • {next.Task}\n{next.Due:ddd, dd MMM}";}
}

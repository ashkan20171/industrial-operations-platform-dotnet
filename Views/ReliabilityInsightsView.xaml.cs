using System.Windows.Controls;
using AshkanIndustrialOperations.Services;
namespace AshkanIndustrialOperations.Views;
public partial class ReliabilityInsightsView : UserControl
{
    public ReliabilityInsightsView(DemoDataService data, Stage6DataService stage6)
    {
        InitializeComponent();
        var rows=data.Machines.Select(m=>{var temp=Math.Clamp((m.Temperature-45)*1.4,0,35);var vib=Math.Clamp((m.Vibration-1)*8,0,35);var oee=Math.Clamp((90-m.Oee)*1.2,0,20);var state=m.Status=="Alarm"?10:m.Status=="Warning"?6:0;var score=Math.Clamp(temp+vib+oee+state,0,100);var driver=m.Vibration>=4?"Elevated vibration":m.Temperature>=70?"Thermal drift":m.Oee<80?"OEE degradation":"Stable condition";var level=score>=60?"High":score>=35?"Medium":"Low";var action=score>=60?"Inspect within current shift":score>=35?"Trend and schedule inspection":"Continue monitoring";return new RiskRow(m.Name,m.Area,score,level,driver,action);}).OrderByDescending(x=>x.Score).ToList();
        RiskGrid.ItemsSource=rows; RiskCount.Text=rows.Count(x=>x.Score>=60).ToString(); LowStockCount.Text=stage6.Parts.Count(x=>x.Stock<=x.ReorderPoint).ToString(); DueCount.Text=stage6.Plans.Count(x=>x.Due<=DateTime.Today.AddDays(7)).ToString();
    }
    private sealed record RiskRow(string Asset,string Area,double Score,string Level,string Driver,string Action);
}

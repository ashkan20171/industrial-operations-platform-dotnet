using System.Windows;
using System.Windows.Controls;
using AshkanIndustrialOperations.Services;

namespace AshkanIndustrialOperations.Views;

public partial class HealthCenterView : UserControl
{
    private readonly DemoDataService _data;
    private readonly Stage6DataService _stage6;
    private readonly OperationalHealthService _health = new();

    public HealthCenterView(DemoDataService data, Stage6DataService stage6)
    {
        InitializeComponent();
        _data = data;
        _stage6 = stage6;
        Refresh();
    }

    private void Refresh()
    {
        var findings = _health.Evaluate(_data, _stage6);
        FindingsGrid.ItemsSource = findings;
        HealthScoreText.Text = $"{_health.CalculatePlantHealth(_data, _stage6)} / 100";
        CriticalCount.Text = findings.Count(x => x.Severity == "Critical").ToString();
        HighCount.Text = findings.Count(x => x.Severity == "High").ToString();
        StockCount.Text = _stage6.Parts.Count(x => x.Stock <= x.ReorderPoint).ToString();
        DueCount.Text = _stage6.Plans.Count(x => x.Due <= DateTime.Today.AddDays(3)).ToString();
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
        _data.Log("Health check executed", "Operational Health Center rules evaluated");
    }
}

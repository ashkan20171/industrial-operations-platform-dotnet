using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AshkanIndustrialOperations.Services;

namespace AshkanIndustrialOperations.Views;

public partial class MainWindow : Window
{
    private readonly DemoDataService _data = new();
    private readonly Stage6DataService _stage6 = new();
    private readonly TelemetrySimulator _telemetry;

    public MainWindow()
    {
        InitializeComponent();
        AppServices.Persistence.Initialize(_data, _stage6);
        _telemetry = new TelemetrySimulator(_data);
        var user = AppServices.Session.CurrentUser;
        UserText.Text = user?.DisplayName ?? "Guest";
        RoleText.Text = user?.Role ?? "Viewer";
        ShowPage("dashboard");
    }

    private void Nav_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string tag }) ShowPage(tag);
    }

    private void ShowPage(string tag)
    {
        if (!AppServices.Permissions.CanNavigate(tag))
        {
            StatusText.Text = $"Access denied • {AppServices.Session.CurrentUser?.Role ?? "Viewer"} role cannot open this module";
            _data.Log("Navigation denied", $"{AppServices.Session.CurrentUser?.Username ?? "anonymous"} → {tag}");
            return;
        }
        MainContent.Content = tag switch
        {
            "assets" => new AssetsView(_data), "alarms" => new AlarmsView(_data),
            "maintenance" => new MaintenanceView(_data), "telemetry" => new TelemetryView(_telemetry),
            "users" => new UsersView(_data), "notifications" => new NotificationsView(_data),
            "audit" => new AuditView(_data), "inventory" => new InventoryView(_stage6),
            "rules" => new AlarmRulesView(_stage6), "scheduler" => new MaintenanceSchedulerView(_stage6),
            "insights" => new ReliabilityInsightsView(_data, _stage6), "health" => new HealthCenterView(_data, _stage6), "connectivity" => new ConnectivityView(AppServices.Connectivity), "portfolio" => new PortfolioView(), "settings" => new SettingsView(),
            _ => new DashboardView(_data, _stage6)
        };
        PageTitle.Text = NavigationCatalog.TitleFor(tag);
        StatusText.Text = $"{PageTitle.Text} • {DateTime.Now:HH:mm:ss}";
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        StatusText.Text = $"Operational snapshot refreshed • {DateTime.Now:HH:mm:ss}";
        _data.Log("Snapshot refreshed", "Manual refresh from command shell");
        if (MainContent.Content is DashboardView dashboard) dashboard.RefreshMetrics();
    }

    private void GlobalSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        var q = GlobalSearch.Text.Trim();
        if (string.IsNullOrWhiteSpace(q)) return;
        var asset = _data.Machines.FirstOrDefault(x => x.Name.Contains(q, StringComparison.OrdinalIgnoreCase));
        var alarm = _data.Alarms.FirstOrDefault(x => x.Code.Contains(q, StringComparison.OrdinalIgnoreCase) || x.Asset.Contains(q, StringComparison.OrdinalIgnoreCase));
        var wo = _data.WorkOrders.FirstOrDefault(x => x.Number.Contains(q, StringComparison.OrdinalIgnoreCase) || x.Asset.Contains(q, StringComparison.OrdinalIgnoreCase));
        StatusText.Text = asset is not null ? $"Found asset {asset.Name} • {asset.Status} • OEE {asset.Oee:F1}%" : alarm is not null ? $"Found {alarm.Code} • {alarm.Asset} • {alarm.State}" : wo is not null ? $"Found {wo.Number} • {wo.Asset} • {wo.Status}" : $"No operational record found for “{q}”";
    }

    private void SignOut_Click(object sender, RoutedEventArgs e)
    {
        _telemetry.Stop();
        AppServices.Persistence.Save(_data, _stage6);
        AppServices.Session.SignOut();
        new LoginWindow().Show();
        Close();
    }
}

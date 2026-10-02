using System.Windows.Controls;
using AshkanIndustrialOperations.Services;
namespace AshkanIndustrialOperations.Views;
public partial class ConnectivityView : UserControl
{
    public ConnectivityView(IndustrialConnectivityService service)
    {
        InitializeComponent();
        ConnectionsGrid.ItemsSource = service.Connections;
        ArchitectureText.Text = service.ArchitectureSummary;
    }
}

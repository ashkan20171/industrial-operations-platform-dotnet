using System.Windows.Controls;
using AshkanIndustrialOperations.Services;
using AshkanIndustrialOperations.ViewModels;
namespace AshkanIndustrialOperations.Views;
public partial class DashboardView : UserControl
{
    private readonly DashboardViewModel _viewModel;
    public DashboardView(DemoDataService data, Stage6DataService stage6){InitializeComponent();_viewModel=new(data,stage6,AppServices.OperationalSummary);DataContext=_viewModel;}
    public void RefreshMetrics()=>_viewModel.Refresh();
}

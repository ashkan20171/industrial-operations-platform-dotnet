using System.Windows;
using System.Windows.Input;
using AshkanIndustrialOperations.Services;
namespace AshkanIndustrialOperations.Views;
public partial class LoginWindow : Window
{
    public LoginWindow()=>InitializeComponent();
    private async void SignIn_Click(object sender,RoutedEventArgs e)=>await SignInAsync();
    private async void Password_KeyDown(object sender,KeyEventArgs e){if(e.Key==Key.Enter) await SignInAsync();}
    private async Task SignInAsync(){ErrorText.Text=string.Empty;var result=await AppServices.Authentication.SignInAsync(Username.Text,Password.Password);if(!result.Success){ErrorText.Text=result.Message;return;}new MainWindow().Show();Close();}
}

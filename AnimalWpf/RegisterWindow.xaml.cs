using AnimalWpf.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimalWpf;

public partial class RegisterWindow : Window
{
    private static readonly Regex PasswordRegex = new Regex(@"^.{0,8}$");

    public RegisterWindow()
    {
        viewModel = new RegisterViewModel();
        InitializeComponent();
        DataContext = viewModel;
    }
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7007/")
    };
    private readonly RegisterViewModel viewModel;

    private async void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.RegisterDto.Password != viewModel.ConfirmPassword)
        {
            viewModel.Message = "Passwords do not match.";
            return;
        }

        var json = JsonSerializer.Serialize(
            viewModel.RegisterDto,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        try
        {
            var response = await httpClient.PostAsync(
                "api/auth/register",
                content);

            if (response.IsSuccessStatusCode)
            {
                viewModel.Message = "Registration successful.";
                return;
            }

            var errorJson = await response.Content.ReadAsStringAsync();

            viewModel.Message = $"Registration failed: {errorJson}";
        }
        catch (HttpRequestException)
        {
            viewModel.Message = "Could not connect to Auth API.";
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordRegexing(PasswordBoxx);

        viewModel.RegisterDto.Password = PasswordBoxx.Password;
    }

    private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        PasswordRegexing(ConfirmPasswordBox);

        viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
    }
    private void OpenLoginWindowButton_Click(object sender, RoutedEventArgs e)
    {
        var loginWindow = new LoginWindow();

        loginWindow.Show();
        Close();
    }

    private void PasswordRegexing(PasswordBox passworBox)
    {
        bool isValid = PasswordRegex.IsMatch(passworBox.Password);

        if (isValid)
        {
            passworBox.ClearValue(Control.BackgroundProperty);
            passworBox.ClearValue(Control.BorderBrushProperty);
        }
        else
        {
            passworBox.Background = new SolidColorBrush(Color.FromRgb(255, 224, 224));
            passworBox.BorderBrush = Brushes.Red;
        }
    }
}

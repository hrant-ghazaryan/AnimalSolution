using AuthDTOs;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;

namespace AnimalWpf;

public partial class LoginWindow : Window
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7184/")
    };

    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var email = LoginEmailTextBox.Text.Trim();
        var password = LoginPasswordBox.Password;

        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            MessageTextBlock.Text = "Email and password are required.";
            return;
        }

        var loginRequest = new LoginRequestDto
        {
            Email = email,
            Password = password
        };

        var json = JsonSerializer.Serialize(
            loginRequest,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        loginProgressBar.Value = 0;
        loginProgressBar.Visibility = Visibility.Visible;
        loginButton.IsEnabled = false;

        try
        {
            var response = await httpClient.PostAsync("api/auth/login", content);

            loginProgressBar.Value = 100;
            await Task.Delay(200);

            if (response.IsSuccessStatusCode)
            {
                MessageTextBlock.Text = "Login successful.";
                var userProfile = new UserMainWindow();
                userProfile.Show();
                Close();
                return;
            }

            var errorJson =
                await response.Content.ReadAsStringAsync();

            MessageTextBlock.Text =
                $"Login failed: {errorJson}";
        }
        catch (HttpRequestException)
        {
            MessageTextBlock.Text =
                "Could not connect to Auth API.";
        }
        finally
        {
            loginProgressBar.Visibility = Visibility.Collapsed;
            loginButton.IsEnabled = true;
        }
    }

    private void OpenRegisterWindowButton_Click(object sender, RoutedEventArgs e)
    {
        var registerWindow = new RegisterWindow();

        registerWindow.Show();
        Close();
    }
}

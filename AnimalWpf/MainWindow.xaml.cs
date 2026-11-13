using AuthDTOs;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using AnimalWpf.ViewModels;

namespace AnimalWpf
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7184/")
        };
        private readonly RegisterViewModel viewModel;

        public MainWindow()
        {
            viewModel = new RegisterViewModel();
            InitializeComponent();
            DataContext = viewModel;
        }

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
            => viewModel.RegisterDto.Password = PasswordBox.Password;

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
            => viewModel.ConfirmPassword = ConfirmPasswordBox.Password;

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = new
            {
                Email = EmailTextBox.Text,
                Password = PasswordBox.Password
            };

            var json = JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageTextBlock.Text = "Login successful.";
                    return;
                }

                var errorJson = await response.Content.ReadAsStringAsync();

                MessageTextBlock.Text = $"Login failed: {errorJson}";
            }
            catch (HttpRequestException)
            {
                MessageTextBlock.Text = "Could not connect to Auth API.";
            }
        }
    }
}
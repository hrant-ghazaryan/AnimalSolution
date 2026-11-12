using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace AnimalWpf
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7184/")
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageTextBlock.Text = "Passwords do not match.";
                return;
            }

            var userData = new
            {
                UserName = UserNameTextBox.Text,
                Email = EmailTextBox.Text,
                Password = PasswordBox.Password
            };
            
            var json = JsonSerializer.Serialize(userData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("api/auth/register", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageTextBlock.Text = "Registration successful.";
                    return;
                }

                var errorJson = await response.Content.ReadAsStringAsync();

                MessageTextBlock.Text = $"Registration failed: {errorJson}";
            }
            catch (HttpRequestException)
            {
                MessageTextBlock.Text = "Could not connect to Auth API.";
            }
        }

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
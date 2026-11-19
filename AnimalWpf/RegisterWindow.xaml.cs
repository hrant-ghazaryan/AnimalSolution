using AnimalWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnimalWpf;

public partial class RegisterWindow : Window
{
    public RegisterWindow()
    {
        viewModel = new RegisterViewModel();
        InitializeComponent();
        DataContext = viewModel;
    }
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7184/")
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
        => viewModel.RegisterDto.Password = PasswordBox.Password;

    private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        => viewModel.ConfirmPassword = ConfirmPasswordBox.Password;

    private void OpenLoginWindowButton_Click(object sender, RoutedEventArgs e)
    {
        var loginWindow = new LoginWindow();

        loginWindow.Show();
        Close();
    }
}

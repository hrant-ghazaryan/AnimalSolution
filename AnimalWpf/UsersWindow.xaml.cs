using AuthDTOs;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace AnimalWpf;

public partial class UsersWindow : Window
{
    private static readonly HttpClient authApiHttpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7007/")
    };

    public ObservableCollection<UserGetDto> UserListItems { get; } = [];

    public UsersWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var users = await authApiHttpClient.GetFromJsonAsync<List<UserGetDto>>(
                "api/Auth/users");

            if (users is null || users.Count is 0)
                return;

            UserListItems.Clear();

            foreach (var user in users)
                UserListItems.Add(user);
        }
        catch (HttpRequestException)
        {
            MessageBox.Show(
                "Could not connect to Auth API.",
                "Users",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}

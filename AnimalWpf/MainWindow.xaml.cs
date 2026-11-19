using AnimalWpf.ViewModels;
using System.Windows;

namespace AnimalWpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddAnimal_Click(object sender, RoutedEventArgs e)
    {
        new AnimalCreateWindow().Show();
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
    }

    private void Register_Click(object sender, RoutedEventArgs e)
    {
        new RegisterWindow().Show();
    }

    private void UserProfile_Click(object sender, RoutedEventArgs e)
    {
        new UserMainWindow().Show();
    }
}

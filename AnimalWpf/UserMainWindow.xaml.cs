using System.Windows;

namespace AnimalWpf;

public partial class UserMainWindow : Window
{
    public UserMainWindow()
    {
        InitializeComponent();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        LoginWindow.LoginedUser = null;
        Close();
    }

    private void AddAnimal_Click(object sender, RoutedEventArgs e)
    {
        var animalCreateWindow = new AnimalCreateWindow();

        animalCreateWindow.Show();
    }
}
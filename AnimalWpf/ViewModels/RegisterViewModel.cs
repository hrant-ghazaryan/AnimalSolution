using AuthDTOs;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnimalWpf.ViewModels;

public class RegisterViewModel : INotifyPropertyChanged
{
    public RegisterRequestDto RegisterDto { get; } = new();

    private string confirmPassword = string.Empty;
    public string ConfirmPassword
    {
        get => confirmPassword;
        set
        {
            confirmPassword = value;
            OnPropertyChanged();
        }
    }

    private string message = string.Empty;
    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Windows;
using System.Windows.Controls;
using AnimalDTOs;
using AnimalModels.Entitites;
using TranslationDTOs;

namespace AnimalWpf;

public partial class AnimalCreateWindow : Window
{
    private string defaultLanguage = "en";
    HttpClient animalClient = new()
    {
        BaseAddress = new Uri("https://localhost:7007/")
    };

    public AnimalCreateWindow()
    {
        InitializeComponent();
        Window_Loaded(this, new RoutedEventArgs());
    }

    async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        lngComboBox.ItemsSource = new List<string> { "hy", "ru", "en" };
        lngComboBox.SelectedItem = defaultLanguage;

        UpdateTranslationsAsync(defaultLanguage);
    }

    async void SelectedLanguageChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedLanguage = lngComboBox.SelectedItem?.ToString();

        if (selectedLanguage is not string)
            return;

        UpdateTranslationsAsync(selectedLanguage);
    }

    public async void UpdateTranslationsAsync(string selectedLanguage)
    {
        var colors = await animalClient.GetFromJsonAsync<TranslationGroupResponseDTO>
           ($"api/Translations/colors?languageCode={selectedLanguage}");

        var genders = await animalClient.GetFromJsonAsync<TranslationGroupResponseDTO>
            ($"api/Translations/genders?languageCode={selectedLanguage}");

        colorComboBox.ItemsSource = colors?.TranslatedWords.ToList();
        colorComboBox.DisplayMemberPath = "Value";
        colorComboBox.SelectedValuePath = "Key";

        genderComboBox.ItemsSource = genders?.TranslatedWords.ToList();
        genderComboBox.DisplayMemberPath = "Value";
        genderComboBox.SelectedValuePath = "Key";
    }

    private async void AnimalCreate_Click(object sender, RoutedEventArgs e)
    {
        if (LoginWindow.LoginedUser is null)
            throw new UnauthorizedAccessException();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.ReadJwtToken(LoginWindow.LoginedUser?.AccessToken);

        var userName = jwt.Claims.
            FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(userName))
            throw new InvalidDataException();

        var color = Enum.TryParse<Color>(
            colorComboBox.SelectedValue?.ToString(), out var parsedColor);
        var gender = Enum.TryParse<AnimalGender>(
            genderComboBox.SelectedValue?.ToString(), out var parsedGender);

        var animalCreateDTO = new AnimalCreateDTO
        {
            Name = nameTextBox.Text,
            Age = int.Parse(ageTextBox.Text),
            Weight = double.Parse(weightTextBox.Text),
            Color = parsedColor,
            Gender = parsedGender,
            OwnedBy = userName
        };

        try
        {
            var response = await animalClient.PostAsJsonAsync("api/Animals", animalCreateDTO);

            if (response.IsSuccessStatusCode)
                messageTextBox.Text = "Animal created successfully.";
            else
                messageTextBox.Text = $"Failed to create animal. Status code: {response.StatusCode}";
        }
        catch(HttpRequestException)
        {
            messageTextBox.Text =
                "Could not connect to Auth API.";
        }
    }
}

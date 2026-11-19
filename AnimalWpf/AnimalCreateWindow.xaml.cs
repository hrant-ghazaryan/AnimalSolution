using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using TranslationDTOs;

namespace AnimalWpf;

public partial class AnimalCreateWindow : Window
{
    private readonly string defaultLanguage = "en";
    HttpClient animalClient = new()
    {
        BaseAddress = new Uri("https://localhost:7150/")
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
}

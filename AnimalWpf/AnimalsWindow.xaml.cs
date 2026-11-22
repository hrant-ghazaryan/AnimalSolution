using AnimalDTOs;
using AnimalExceptions;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace AnimalWpf;

public partial class AnimalsWindow : Window
{
    private static readonly string defaultLanguage = "en";
    private static string? selectedLanguage;
    private readonly HttpClient animalClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7150/")
    };

    public readonly HttpClient translationClient = new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7274/")
    };

    public ObservableCollection<AnimalResponseDTO> Animals { get; } = [];

    public AnimalsWindow()
    {
        InitializeComponent();
        DataContext = this;
        cmbLanguage.ItemsSource = new List<string> { "en", "ru", "hy" };
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        selectedLanguage = cmbLanguage.SelectedItem?.ToString();
        if (string.IsNullOrWhiteSpace(selectedLanguage))
        {
            cmbLanguage.Text = defaultLanguage;
            selectedLanguage = cmbLanguage.Text;
        }

        var animalsRequest = new HttpRequestMessage(HttpMethod.Get, "api/animals");
        animalsRequest.Headers.AcceptLanguage.ParseAdd(Uri.EscapeDataString(selectedLanguage));

        var animalsResponse = await animalClient.SendAsync(animalsRequest);
        var animalList = await animalsResponse.Content.ReadFromJsonAsync<List<AnimalResponseDTO>>();

        AddAnimalsToDataGrid(animalList);
    }

    private async void btnSearch_Click(object sender, RoutedEventArgs e)
    {
        var searchWord = txtSearch.Text;

        if (string.IsNullOrWhiteSpace(searchWord))
            return;

        List<AnimalResponseDTO>? searchedAnimalsResponse;
        try
        {
            searchedAnimalsResponse = await animalClient.GetFromJsonAsync<List<AnimalResponseDTO>>
            ($"api/animals/search?word={Uri.EscapeDataString(searchWord)}&languageCode={Uri.EscapeDataString(selectedLanguage!)}");
        }
        catch (Exception)
        {
            throw new Exception("Error occurred while searching for animals.");
        }
        
        AddAnimalsToDataGrid(searchedAnimalsResponse);
    }

    private async void LanguageSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedLanguage = cmbLanguage.SelectedItem?.ToString();
        List<AnimalResponseDTO> animals = Animals.ToList();

        if (animals is null || animals.Count == 0)
            return;

        if (string.IsNullOrWhiteSpace(pageCounttxt.ToString()) && 
            string.IsNullOrWhiteSpace(animalsCounttxt.ToString()))
        {
            var pagedAnimals = await GetPagedAnimals();
            await TranslateResultedAnimals(pagedAnimals);
        }

        else if (!string.IsNullOrWhiteSpace(txtSearch.Text.ToString()))
        {
            var searchedAnimalsResponse = await GetSearchedAnimals();
            await TranslateResultedAnimals(searchedAnimalsResponse);
        }

        else 
            await TranslateResultedAnimals(animals!);
    }

    private async void btnGetPages_Click(object sender, RoutedEventArgs e)
    {
        var animalsTranslated = await GetPagedAnimals();

        AddAnimalsToDataGrid(animalsTranslated);
    }

    private async void btnReset_Click(object sender, RoutedEventArgs e)
    {
        selectedLanguage = cmbLanguage.SelectedItem?.ToString();

        List<AnimalResponseDTO>? animalsToDefaultLanguage = await animalClient.GetFromJsonAsync<List<AnimalResponseDTO>>("api/animals");

        var response = await translationClient.PostAsJsonAsync(
            $"api/Translations/translateanimals?languageCode={Uri.EscapeDataString(selectedLanguage!)}",
            animalsToDefaultLanguage);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Translation API error {(int)response.StatusCode}: {await response.Content.ReadAsStringAsync()}");

        var animalsTranslated = await response.Content.ReadFromJsonAsync<List<AnimalResponseDTO>>();

        AddAnimalsToDataGrid(animalsTranslated);
    }

    private void AddAnimalsToDataGrid(List<AnimalResponseDTO>? animals)
    {
        if (animals is null || animals.Count is 0)
            return;

        Animals.Clear();

        foreach (var animal in animals)
            Animals.Add(animal);
    }

    private async Task<List<AnimalResponseDTO>> GetPagedAnimals()
    {
        var pages = pageCounttxt.Text;
        var animalsCount = animalsCounttxt.Text;

        var pagesAsInt = int.TryParse(pages, out int pageNumber);
        var animalsCountAsInt = int.TryParse(animalsCount, out int countNumber);

        if (!pagesAsInt || !animalsCountAsInt)
            throw new AnimalInvalidData("Invalid input data for pages or animals count");

        var animalsByPage = await animalClient.GetFromJsonAsync<List<AnimalResponseDTO>>
            ($"api/animals/pages?page={pageNumber}&count={countNumber}");

        return animalsByPage ?? new List<AnimalResponseDTO>();
    }
    private async Task<List<AnimalResponseDTO>> GetSearchedAnimals()
    {
        var searchWord = txtSearch.Text;
        List<AnimalResponseDTO>? searchedAnimalsResponse;
        try
        {
            searchedAnimalsResponse = await animalClient.GetFromJsonAsync<List<AnimalResponseDTO>>
            ($"api/animals/search?word={Uri.EscapeDataString(searchWord)}&languageCode={Uri.EscapeDataString(selectedLanguage!)}");
        }
        catch (Exception)
        {
            throw new Exception("Error occurred while searching for animals.");
        }

        return searchedAnimalsResponse ?? [];
    }

    private async Task TranslateResultedAnimals(List<AnimalResponseDTO> animals)
    {
        var translateResponse = await translationClient.PostAsJsonAsync(
            $"api/Translations/translateanimals?languageCode={Uri.EscapeDataString(selectedLanguage!)}",
            animals);

        if (!translateResponse.IsSuccessStatusCode)
            throw new Exception($"Translation API error {(int)translateResponse.StatusCode}: {await translateResponse.Content.ReadAsStringAsync()}");

        var animalsTranslated = await translateResponse.Content.ReadFromJsonAsync<List<AnimalResponseDTO>>();

        AddAnimalsToDataGrid(animalsTranslated);
    }

}

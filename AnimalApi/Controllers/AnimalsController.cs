using AnimalModels.Entitites;
using AnimalServices.Intefaces;
using Microsoft.AspNetCore.Mvc;
using AnimalDTOs;

namespace AnimalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService animalService;
    private readonly ITranslationApiClient translationApiClient;
    private readonly IConfiguration configuration;

    public AnimalsController(
        IAnimalService animalService,
        ITranslationApiClient translationApiClient,
        IConfiguration configuration)
    {
        this.animalService = animalService;
        this.translationApiClient = translationApiClient;
        this.configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AnimalCreateDTO dto)
    {
        await animalService.Add(dto);

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id, [FromBody] AnimalCreateDTO dto)
    {
        await animalService.Update(id, dto);

        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AnimalCreateDTO>> Get(int id)
    {
        var animal = await animalService.Get(id);

        return Ok(animal);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id)
    {
        await animalService.Remove(id);

        return NoContent();
    }

    [HttpPatch("{id:int}/close")]
    public async Task<IActionResult> Close(int id)
    {
        await animalService.Close(id);

        return NoContent();
    }

    [HttpGet("pages")]
    public async Task<ActionResult<IEnumerable<AnimalCreateDTO>>> GetAnimalByPageAndCount(
        [FromQuery] int page, [FromQuery] int count)
    {
        var animals = await animalService.GetAnimalByPageAndCount(page, count);

        return Ok(animals);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<AnimalCreateDTO>>> SearchBy(
        [FromQuery] string word)
    {
        var animals = await animalService.SearchBy(word);

        return Ok(animals);
    }

    [HttpGet]
    public async Task<ActionResult<AnimalListResponseDTO>> GetAll(
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage)
    {
        var languageCode = GetLanguageCode(acceptLanguage);

        var colorTranslations = await translationApiClient.GetEnumTranslationsAsync<Color>(
                languageCode);

        var genderTranslations = await translationApiClient.GetEnumTranslationsAsync<AnimalGender>(
                languageCode);

        var animals = await animalService.GetAll();

        var result = animals.Select(animal =>
        {
            var colorCode = animal.Color.ToString();
            var genderCode = animal.Gender.ToString();

            var colorDisplayName = colorTranslations.TryGetValue(
                colorCode,
                out var translatedColor)
                ? translatedColor
                : colorCode;

            var genderDisplayName = genderTranslations.TryGetValue(
                genderCode,
                out var translatedGender)
                ? translatedGender
                : genderCode;

            return new AnimalResponseDTO
            {
                Name = animal.Name,
                Age = animal.Age,
                Weight = animal.Weight,
                Color = colorDisplayName,
                Gender = genderDisplayName
            };
        }).ToList();

        return Ok(new AnimalListResponseDTO
        {
            Count = result.Count(),
            Animals = result
        });
    }

    private string GetLanguageCode(string? acceptLanguage)
    {
        var defaultLanguageCode =
            configuration["Localization:DefaultLanguageCode"]
            ?? throw new InvalidOperationException(
                "Localization default language is not configured.");

        if (string.IsNullOrWhiteSpace(acceptLanguage))
            return defaultLanguageCode;

        var firstLanguage = acceptLanguage
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault();

        if (string.IsNullOrWhiteSpace(firstLanguage))
            return defaultLanguageCode;

        return firstLanguage
            .Split(';')[0]
            .Split('-')[0]
            .Trim()
            .ToLowerInvariant();
    }
}

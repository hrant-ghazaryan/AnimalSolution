using AnimalModels.Entitites;
using Microsoft.AspNetCore.Mvc;
using TranslationDTOs;

namespace AnimalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationsController : ControllerBase
{
    private readonly ITranslationApiClient translationApiClient;

    public TranslationsController(ITranslationApiClient translationApiClient)
        => this.translationApiClient = translationApiClient;

    [HttpGet("colors")]
    public Task<ActionResult<TranslationGroupResponseDTO>> GetColors(
        [FromQuery] string languageCode = "hy")
        => GetEnumTranslations<Color>(languageCode);

    [HttpGet("genders")]
    public Task<ActionResult<TranslationGroupResponseDTO>> GetGenders(
        [FromQuery] string languageCode = "hy")
        => GetEnumTranslations<AnimalGender>(languageCode);

    public async Task<ActionResult<TranslationGroupResponseDTO>> GetEnumTranslations<T>(
        string languageCode) where T : Enum
    {
        if (string.IsNullOrWhiteSpace(languageCode))
            return BadRequest("The languageCode query parameter is required.");

        var normalizedLanguageCode = languageCode
            .Split('-', StringSplitOptions.RemoveEmptyEntries)[0]
            .Trim()
            .ToLowerInvariant();

        var values = await translationApiClient
            .GetEnumTranslationsAsync<T>(normalizedLanguageCode);

        return Ok(new TranslationGroupResponseDTO
        {
            EnumType = typeof(T).Name,
            LanguageCode = normalizedLanguageCode,
            TranslatedWords = new Dictionary<string, string>(values)
        });
    }
}

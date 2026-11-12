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
        [FromQuery] string languageCode = "hy",
        CancellationToken cancellationToken = default)
        => GetEnumTranslations<Color>(languageCode, cancellationToken);

    [HttpGet("genders")]
    public Task<ActionResult<TranslationGroupResponseDTO>> GetGenders(
        [FromQuery] string languageCode = "hy",
        CancellationToken cancellationToken = default)
        => GetEnumTranslations<AnimalGender>(languageCode, cancellationToken);

    private async Task<ActionResult<TranslationGroupResponseDTO>> GetEnumTranslations<TEnum>(
        string languageCode,
        CancellationToken cancellationToken)
        where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(languageCode))
            return BadRequest("The languageCode query parameter is required.");

        var normalizedLanguageCode = languageCode
            .Split('-', StringSplitOptions.RemoveEmptyEntries)[0]
            .Trim()
            .ToLowerInvariant();

        var values = await translationApiClient
            .GetEnumTranslationsAsync<TEnum>(normalizedLanguageCode);

        return Ok(new TranslationGroupResponseDTO
        {
            ResourceType = typeof(TEnum).Name,
            LanguageCode = normalizedLanguageCode,
            Values = new Dictionary<string, string>(values)
        });
    }
}

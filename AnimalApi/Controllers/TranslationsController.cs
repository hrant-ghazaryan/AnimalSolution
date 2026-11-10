using Microsoft.AspNetCore.Mvc;
using AnimalApi;
using TranslationDTOs;

namespace AnimalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationsController : ControllerBase
{
    private readonly ITranslationApiClient translationApiClient;

    public TranslationsController(ITranslationApiClient translationApiClient)
        => this.translationApiClient = translationApiClient;

    [HttpGet("{word}")]
    public async Task<ActionResult<TranslationResponseDTO>> GetTranslations(
    string word,
    CancellationToken cancellationToken)
    {
        var result = await translationApiClient.GetTranslationsAsync(
            word,
            cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}

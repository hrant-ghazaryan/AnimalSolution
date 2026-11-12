using Microsoft.AspNetCore.Mvc;
using TranslationDTOs;
using TranslationServices;

namespace TranslationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationsController : ControllerBase
{
    private readonly ITranslationService translationService;

    public TranslationsController(ITranslationService translationService)
        => this.translationService = translationService;

    [HttpGet("resources/{resourceType}")]
    public async Task<ActionResult<TranslationGroupResponseDTO>> GetResourceTranslations(
        string resourceType,
        [FromQuery] string languageCode,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(resourceType) ||
            string.IsNullOrWhiteSpace(languageCode))
        {
            return BadRequest();
        }

        var result = await translationService.GetResourceTranslationsAsync(
            resourceType,
            languageCode,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTranslation(
        [FromBody] TranslationCreateDTO translationCreateDTO,
        CancellationToken cancellationToken)
    {
        await translationService.AddTranslationAsync(
            translationCreateDTO,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetResourceTranslations),
            new
            {
                resourceType = translationCreateDTO.ResourceType,
                languageCode = translationCreateDTO.LanguageCode
            },
            translationCreateDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTranslation(
        int id,
        CancellationToken cancellationToken)
    {
        await translationService.DeleteTranslationAsync(
            id,
            cancellationToken);

        return Ok();
    }
}
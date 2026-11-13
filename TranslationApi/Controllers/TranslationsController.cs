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

    [HttpGet("resources/{enumType}")]
    public async Task<ActionResult<TranslationGroupResponseDTO>> GetEnumTranslations(
        string enumType, [FromQuery] string languageCode)
    {
        if (string.IsNullOrWhiteSpace(enumType) ||
            string.IsNullOrWhiteSpace(languageCode))
        {
            return BadRequest();
        }

        var result = await translationService.GetEnumTranslationsAsync(
            enumType, languageCode);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTranslation(
        [FromBody] TranslationCreateDTO translationCreateDTO)
    {
        await translationService.AddTranslationAsync(
            translationCreateDTO);

        return CreatedAtAction(
            nameof(GetEnumTranslations),
            new
            {
                enumType = translationCreateDTO.EnumType,
                languageCode = translationCreateDTO.LanguageCode
            },
            translationCreateDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTranslation(int id)
    {
        await translationService.DeleteTranslationAsync(id);

        return Ok();
    }
}
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

    [HttpGet("{word}")]
    public async Task<ActionResult<TranslationResponseDTO>> GetTranslations(string word)
    {
        var result = await translationService.GetTranslationsAsync(word);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTranslation(
        [FromBody] TranslationCreateDTO translationCreateDTO)
    {
        await translationService.AddTranslationAsync(translationCreateDTO);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTranslation(int id)
    {
        await translationService.DeleteTranslationAsync(id);

        return NoContent();
    }
}
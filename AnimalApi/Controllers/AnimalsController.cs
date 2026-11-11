using AnimalServicesLibrary.Intefaces;
using Microsoft.AspNetCore.Mvc;
using RequestDTOs;

namespace AnimalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService animalService;
    public AnimalsController(IAnimalService animalService)
        => this.animalService = animalService;

    [HttpPost]
    public IActionResult Add(AnimalDTO dto)
    {
        animalService.Add(dto);
        return Ok();
    }
}

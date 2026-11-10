using AnimalServicesLibrary.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalService animalService;
    public AnimalsController(IAnimalService animalService)
        => this.animalService = animalService;


}

using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Animals;


[ApiController]
[Route("api/animlas")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpPost]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        var success = _animalService.AddAnimal(dto);
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        var animals = _animalService.GetAllAnimals(orderBy);
        return Ok(animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal([FromRoute] int id)
    {
        var animal = _animalService.GetOneAnimal(id);
        return Ok(animal);
    }
    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(Animal animal)
    {
        var affected = _animalService.UpdateAnimal(animal);
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var affected = _animalService.DeleteAnimal(id);
        return NoContent();
    }
}
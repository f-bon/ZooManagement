using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooManagement.Database;
using ZooManagement.Models;

namespace ZooManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ZooController : ControllerBase
{

    private readonly ILogger<ZooController> _logger;
    private ZooContext _zooContext;

     public ZooController(ILogger<ZooController> logger, ZooContext zooContext)
    {
        _logger = logger;
        _zooContext = zooContext;
    }


    [HttpGet("{id}")]
    public ActionResult Get(int? id)
    {
        try
        {
            var dateFormat  = CultureInfo.CreateSpecificCulture("en-DE");
            var animal = _zooContext.Animal
                .Include(animal => animal.Species)
                .Select(animal => new
                {
                    animal.Id,
                    animal.Name,
                    DateOfBirth = animal.DateOfBirth.ToString("d",dateFormat),
                    DateAcquired = animal.DateAcquired.ToString("d",dateFormat),
                    animal.Sex,
                    animal.Species.SpeciesName,
                    animal.Species.Classification
                })
                .FirstOrDefault(animal => animal.Id == id);           

            if (animal is null)
            {
                _logger.LogInformation($"No animal in system with this ID: {id}");
                return NotFound($"No animal in system with this ID: {id}");
            }
            _logger.LogInformation($"Found animal with id: {animal.Id}");
            var animalDto = new AnimalDto(animal.Name, DateOnly.Parse(animal.DateOfBirth), DateOnly.Parse(animal.DateAcquired), animal.Sex, animal.SpeciesName, animal.Classification);
            return Ok(animalDto);
        }
        catch (FormatException)
        {
            _logger.LogError("Date is not in the correct format.");
            throw new FormatException("Invalid date format.");
        }

    }

    [HttpGet]
    public ActionResult GetAllSpecies ()
    {
        try 
        {
            var allSpecies = _zooContext.Species.ToList();
            return Ok(allSpecies);
        }
        catch (Exception)
        {
            _logger.LogError("Error finding species list");
            throw new Exception("Error finding species list");
        }
    }
    
    [HttpPost]
    public ActionResult AddAnimal([FromBody] AnimalDto animalDto)
    {
        try
        {
            Species species = new(animalDto.SpeciesName, animalDto.Classification);
            if (!_zooContext.Species.Any(s => s.SpeciesName == animalDto.SpeciesName))
            {
                _zooContext.Species.Add(species);
                _zooContext.SaveChanges();
            }
            Animal animal = new(animalDto.Name, animalDto.DateOfBirth, animalDto.DateAcquired, animalDto.Sex, species);
            _zooContext.Animal.Add(animal);
            _zooContext.SaveChanges();
            return Ok($" and added to the database.");
        }
        catch (Exception)
        {
            _logger.LogError("Error adding animal/species to the database.");
            throw new Exception("Error adding animal/species to the database.");
        }
    }
}
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
            var animal = _zooContext.Animal
                .Include(animal => animal.Species)
                .Select(animal => new
                {
                    animal.Id,
                    animal.Name,
                    DateOfBirth = DateTime.ParseExact(animal.DateOfBirth.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    DateAcquired = DateTime.ParseExact(animal.DateAcquired.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
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
            var animalDto = new AnimalDto(animal.Id, animal.Name, animal.DateOfBirth, animal.DateAcquired, animal.Sex, animal.SpeciesName, animal.Classification);
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

}
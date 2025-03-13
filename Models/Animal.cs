using System.ComponentModel.DataAnnotations;

namespace ZooManagement.Models;

public class Animal
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public DateOnly DateAcquired { get; set; }
    [Required]
    public string Sex { get; set; }
    public Species Species { get; set; }
    public Animal(Animal animal, Species species)
    {
        Id = animal.Id;
        Name = animal.Name;
        DateOfBirth = animal.DateOfBirth;
        DateAcquired = animal.DateAcquired;
        Sex = animal.Sex;
        Species = species;
    }
    public Animal()
    {

    }
    public Animal( string name, DateOnly dateOfBirth, DateOnly dateAcquired, string sex, Species species)
    {
        // Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        DateAcquired = dateAcquired;
        Sex = sex;
        Species = species;
    }

}

using System.ComponentModel.DataAnnotations;

namespace ZooManagement.Models;

public class Animal
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Required]
    public DateTime DateAcquired { get; set; }
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
    public Animal( string name, DateTime dateOfBirth, DateTime dateAcquired, string sex, Species species)
    {
        // Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        DateAcquired = dateAcquired;
        Sex = sex;
        Species = species;
    }

}

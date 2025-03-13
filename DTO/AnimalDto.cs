namespace ZooManagement.Models;

public class AnimalDto
{
    // public int Id { get; set; }    
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateAcquired { get; set; }
    public string Sex { get; set; }
    public string SpeciesName { get; set; }
    public string Classification {get;set;}
    
    public AnimalDto()
    {

    }
    public AnimalDto( string name, DateTime dateOfBirth, DateTime dateAcquired, string sex, string speciesName, string classification)
    {
    //    Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        DateAcquired = dateAcquired;
        Sex = sex;
        SpeciesName = speciesName;
        Classification = classification;
    }
}
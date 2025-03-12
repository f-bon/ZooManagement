using System.ComponentModel.DataAnnotations;

namespace ZooManagement.Models;
public class Species{
    public int Id { get; set; }
    [Required]
    public string SpeciesName { get; set; }
    [Required]
    public string Classification { get; set; }
    public Species(Species species){
        Id = species.Id;
        SpeciesName = species.SpeciesName;
        Classification = species.Classification;
    }

    public Species(){}
}
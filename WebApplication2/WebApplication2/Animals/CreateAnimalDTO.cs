using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Animals;

public class CreateAnimalDTO
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }
}
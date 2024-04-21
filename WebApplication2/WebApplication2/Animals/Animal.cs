using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Animals;

public class Animal
{
    [Required]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Area { get; set; }
}
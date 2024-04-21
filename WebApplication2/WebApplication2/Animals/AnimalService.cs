namespace WebApplication2.Animals;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public int AddAnimal(Animal animal);
    public Animal GetOneAnimal(int id);
    public int UpdateAnimal(Animal animal);
    public int DeleteAnimal(int id);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.GetAllAnimals(orderBy);
    }

    public int AddAnimal(Animal animal)
    {
        return _animalRepository.CreateAnimal(animal);
    }

    public Animal GetOneAnimal(int id)
    {
        return _animalRepository.GetOneAnimal(id);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalRepository.UpdateAnimal(animal);
    }

    public int DeleteAnimal(int id)
    {
        return _animalRepository.DeleteAnimal(id);
    }
}
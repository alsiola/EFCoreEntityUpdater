# EFCoreEntityUpdater
Implementation of EF6's CurrentValues.SetValues() for EF Core

#Usage

```c#
class Dog
{
  public int Id {get; set;}
  public string Name {get; set;}
  public string Breed {get;set;}
}

Dog original = new Dog() { Id = 1, Name = "Fido", Breed = "Labrador" }
Dog updated = new Dog() { Id = 0, Name = "Lassie", Breed = "Collie" }

original.UpdateProperties(updated);

Console.WriteLine(original.Id); // 1 - unchanged
Console.WriteLine(original.Name); // Lassie - updated
Console.WriteLine(original.Breed); // Collie - updated
```

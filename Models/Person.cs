using System.Text.Json.Serialization;

namespace person_dogs;
class Person
{
    public Guid id {get;set;}
    public string Nombre {get;set;}
    public string Apellido {get;set;}
  
    [JsonIgnore]
    public ICollection<Dog> dogs {get;set;}

     
}

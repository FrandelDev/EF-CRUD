
using System.Text.Json.Serialization;

namespace person_dogs;
class Dog
{
    public Guid id {get;set;}
    public Guid AmoId {get;set;}
    public string Nombre {get;set;}
    public string Raza {get;set;}
    public int Age {get;set;}

    public Person Amo {get;set;}
   
  
}
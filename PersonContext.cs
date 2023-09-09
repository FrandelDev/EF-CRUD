
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace person_dogs;

class PersonContext : DbContext
{
    public DbSet<Person> Persons {get;set;}
    public DbSet<Dog> Dogs {get;set;}

    public PersonContext(DbContextOptions<PersonContext> options) :base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        List<Person> personInit = new List<Person>();
        personInit.Add(new Person(){
        id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
        Nombre = "Frandel",
        Apellido = "Corporan",
    });
        modelBuilder.Entity<Person>(person => {
            person.HasKey(pk => pk.id);
            person.HasMany(prop => prop.dogs).WithOne(prop => prop.Amo).HasForeignKey(fk => fk.AmoId);
            person.Property(prop => prop.Nombre).IsRequired();
            person.Property(prop => prop.Apellido).IsRequired();
            person.HasData(personInit);
        });

List<Dog> dogInit = new List<Dog>();
dogInit.Add(new Dog(){
    id = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfbd11"),
    AmoId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"),
    Nombre = "Chubi",
    Age = 5
});
        modelBuilder.Entity<Dog>(dog => {
            dog.HasKey(pk => pk.id);
            dog.Property(prop => prop.Nombre).IsRequired();
            dog.Property(prop => prop.Age);
            dog.Property(prop => prop.Raza).IsRequired(false);
            dog.HasData(dogInit);
        });
    }
}

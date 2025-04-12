using DemoMapster.Model;

namespace DemoMapster
{
    public static class PersonRepository
    {
        public static IQueryable<Person> GetPerson()
        {
            var persons = new List<Person>()
            {
                new Person() { Id= Guid.NewGuid(), FirstName="Teste01", LastName = "lastName01" , Age=30 ,CreatedAt = DateTime.Now },
                new Person() { Id= Guid.NewGuid(), FirstName="Teste02", LastName = "lastName02" , Age=25 ,CreatedAt = DateTime.Now },
                new Person() { Id= Guid.NewGuid(), FirstName="Teste3", LastName = "lastName03" , Age=20 ,CreatedAt = DateTime.Now },
            };

            return persons.AsQueryable();
        }

        public static Person SavePerson(Person person)
        {
            person.Id = Guid.NewGuid();
            person.CreatedAt = DateTime.Now;
            return person;
        }
    }
}

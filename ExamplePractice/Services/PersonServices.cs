using ExamplePractice.Db;
using ExamplePractice.IServices;

namespace ExamplePractice.Services
{
    public class PersonServices : IPersonServices
    {
        private readonly ApplicationDbContext _context;

        public PersonServices(ApplicationDbContext context)
        {
                _context = context; 
        }
        public List<Person> GetAllPersons()
        {
            _context.Persons.Join(Person, )
            return _context.Persons.ToList();
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public Person SetPerson(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return person;
        }
    }
}

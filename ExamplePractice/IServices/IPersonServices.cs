using ExamplePractice.Db;

namespace ExamplePractice.IServices
{
    public interface IPersonServices
    {
        Person GetPerson(int id);   

        Person SetPerson(Person person);    

        List<Person> GetAllPersons();   
    }
}

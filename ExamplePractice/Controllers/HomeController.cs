using ExamplePractice.Db;
using ExamplePractice.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ExamplePractice.Controllers
{
    [ApiController]
    [Route("api/home")]
    [Produces("application/json")]
    public class HomeController : Controller
    {
        private readonly IPersonServices _personServices;

        public HomeController(IPersonServices personServices)
        {
            _personServices = personServices;
        }
        [HttpGet("/")]
        public List<Db.Person> Index()
        {
            int y = 100;
            Console.WriteLine((getData(out y)));
            return _personServices.GetAllPersons();
        }

        [HttpPost("/save")]
        public Person Save(Person person)
        {
            return _personServices.SetPerson(person);
        }

        [HttpPut("/update/{Id}")]
        public Person Update(int Id, Person person)
        {
            return new Person();
        }

        public int getData(out int x)
        {
            x = 10;
            return x;
        }

    }
}

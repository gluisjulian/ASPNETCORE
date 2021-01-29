using REST_withASPNETCORE5.Model;
using System.Collections.Generic;
namespace REST_withASPNETCORE5.Business.Implementations
{
    public interface IPersonBusiness
    {
        Person Create(Person person);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Person person);

        void Delete(long id);
    }
}

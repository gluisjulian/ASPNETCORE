using REST_withASPNETCORE5.Model;
using REST_withASPNETCORE5.Repository.Implementations;
using System.Collections.Generic;

namespace REST_withASPNETCORE5.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        //variavel
        private readonly IPersonRepository _repository;

        //construtor
        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;

        }

        public List<Person> FindAll()
        {

            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}

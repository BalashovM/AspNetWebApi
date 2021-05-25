using System.Collections.Generic;
using System.Linq;
using Lesson03_Practice.Data.Interfaces;
using Lesson03_Practice.Models;

namespace Lesson03_Practice.Data.Implementation
{
    public class PersonRepo: IPersonRepo
    {
        private readonly IPersonData _personData;

        public PersonRepo(IPersonData personData)
        {
            _personData = personData;
        }

        public int GetCount()
        {
            return _personData.GetCount();
        }
        public Person GetItem(int id)
        {
            return _personData.Persons.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Person> GetItemByName(string name)
        {
            return from person in _personData.Persons where person.FirstName == name select person;
        }

        public IEnumerable<Person> GetItems(int skip, int take)
        {
            return _personData.Persons.Skip(skip).Take(take);
        }

        public void Create(Person item)
        {
            _personData.Persons.Add(item);
        }
        public void Update(Person item)
        {
            var person = _personData.Persons.FirstOrDefault(x => x.Id == item.Id);
            if (person == null) return;
            person.Age = item.Age;
            person.Company = item.Company;
            person.Email = item.Email;
            person.FirstName = item.FirstName;
            person.LastName = item.LastName;
        }

        public void Delete(int id)
        {
            var person = _personData.Persons.FirstOrDefault(x => x.Id == id);
            _personData.Persons.Remove(person);
        }
    }
}
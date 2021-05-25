using Lesson03_Practice.Data.Interfaces;
using Lesson03_Practice.Domain.Interfaces;
using Lesson03_Practice.Models;
using Lesson03_Practice.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03_Practice.Domain.Implementation
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepo _personRepo;

        public PersonManager(IPersonRepo personRepo)
        {
            _personRepo = personRepo;
        }

        public Person GetItem(int id)
        {
            return _personRepo.GetItem(id);
        }

        public List<Person> GetItemByName(string name)
        {
            List<Person> persons = _personRepo.GetItemByName(name).ToList();
            return persons;
        }

        public List<Person> GetAll(int skip, int take)
        {
            List<Person> persons = _personRepo.GetItems(skip, take).ToList();
            return persons;
        }

        public int Create(PersonDto item)
        {
            var count = _personRepo.GetCount();
            Person person = new Person()
            {
                FirstName = item.FirstName,
                LastName = item.LastName,
                Company = item.Company,
                Email = item.Email,
                Age = item.Age,
                Id = ++count
            };
            _personRepo.Create(person);
            return count;
        }

        public void Update(Person item)
        {
            _personRepo.Update(item);
        }

        public void Delete(int id)
        {
            _personRepo.Delete(id);
        }
    }
}
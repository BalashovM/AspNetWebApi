using Lesson03_Practice.Models;
using Lesson03_Practice.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson03_Practice.Domain.Interfaces
{
    public interface IPersonManager
    {
        Person GetItem(int id);
        List<Person> GetItemByName(string name);
        List<Person> GetAll(int skip, int take);
        int Create(PersonDto item);
        void Update(Person item);
        void Delete(int id);
    }
}

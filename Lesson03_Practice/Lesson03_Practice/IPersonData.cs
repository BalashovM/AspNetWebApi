using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson03_Practice.Models;

namespace Lesson03_Practice
{
    public interface IPersonData
    {
        List<Person> Persons { get; set; }
        int GetCount();
    }
}

using System.Collections.Generic;

namespace Lesson03_Practice.Data
{
    public interface IRepoBase<T>
    {
        T GetItem(int id);
        IEnumerable<T> GetItemByName(string name);
        IEnumerable<T> GetItems(int skip, int take);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        int GetCount();
    }
}
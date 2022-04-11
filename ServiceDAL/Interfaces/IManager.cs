using System;
using System.Collections.Generic;

namespace ServiceDAL.Interfaces
{
    public interface IManager<T> : IDisposable where T : class
    {
        public IList<T> GetAll();
        public T Get(int id);
        public T Add(T obj);
        public void Update(T obj);
        public bool Delete(int id);
        new void Dispose();
    }
}

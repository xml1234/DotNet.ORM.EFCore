using System.Collections;
using System.Collections.Generic;

namespace Demo.Web.Service
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
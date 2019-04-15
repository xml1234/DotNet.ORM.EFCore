using System.Collections.Generic;
using System.Linq;
using EFCore.DomainModels;

namespace Demo.Web.Service
{
    public class InMemoryRepository:IRepository<Province>
    {
        private readonly List<Province> _provinces;

        public InMemoryRepository()
        {
            _provinces= new List<Province>
            {
                new Province
                {
                    Id=1,
                    Population = 123
                },
                new Province
                {
                    Id=2,
                    Population = 234
                }
            };
        }

        public IEnumerable<Province> GetAll()
        {
            return _provinces;
        }

        public Province GetById(int id)
        {
            return _provinces.FirstOrDefault(x => x.Id == id);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Pub.Models;
using Pub.Repositories.Contracts;
using Pub.Services.Contracts;

namespace Pub.Services
{
    public class BeverageService : IBeverageService
    {

        private readonly IBeverageRepository _beverageRepository;

        public BeverageService(IBeverageRepository beverageRepository)
        {
            _beverageRepository = beverageRepository;
        }

        public IEnumerable<Beverage> GetAllBeverages()
        {
            return _beverageRepository.GetAll().ToList();
        }

        public Beverage GetByName(string name)
        {
            return _beverageRepository.FindByName(name);
        }
    }
}

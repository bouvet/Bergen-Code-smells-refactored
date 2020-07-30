using System.Collections.Generic;
using Pub.Models;

namespace Pub.Repositories.Contracts
{
    public interface IBeverageRepository
    {
        IEnumerable<Beverage> GetAll();
        Beverage FindByName(string name);
    }
}

using System.Collections.Generic;
using Pub.Models;

namespace Pub.Services.Contracts
{
    public interface IBeverageService
    {
        IEnumerable<Beverage> GetAllBeverages();
        Beverage GetByName(string name);
    }
}

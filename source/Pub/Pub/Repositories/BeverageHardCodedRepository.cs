using System;
using System.Collections.Generic;
using System.Linq;
using Pub.Models;
using Pub.Repositories.Contracts;

namespace Pub.Repositories
{
    public class BeverageHardCodedRepository : IBeverageRepository
    {
        private readonly List<Beverage> _beverages;

        public BeverageHardCodedRepository()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "white stuff", UnitPrice = 65 },
                new Ingredient { Name ="grenadine", UnitPrice = 10 },
                new Ingredient { Name = "juice", UnitPrice = 10 },
                new Ingredient { Name = "green stuff", UnitPrice = 10 },
                new Ingredient { Name = "tonic water", UnitPrice = 20 },
                new Ingredient { Name = "gin", UnitPrice = 85 }
            };

            _beverages = new List<Beverage>
            {
                new BeerCider { Name = "hansa", UnitPrice = 74, AllowStudentDiscount = true, MaxOrderNumber = -1 },
                new BeerCider { Name = "grans", UnitPrice = 103, AllowStudentDiscount = true, MaxOrderNumber = -1 },
                new BeerCider { Name = "strongbow", UnitPrice = 110, AllowStudentDiscount = true, MaxOrderNumber = -1 },
                new Cocktail { Name = "gt", AllowStudentDiscount = false, MaxOrderNumber = 2,
                    Ingredients = new List<(decimal amount, Ingredient ingredient)>
                    {
                        (1.0m, ingredients.FirstOrDefault(x => x.Name.Equals("gin"))),
                        (1.0m, ingredients.FirstOrDefault(x => x.Name.Equals("tonic water"))),
                        (1.0m, ingredients.FirstOrDefault(x => x.Name.Equals("green stuff")))
                    }},
                new Cocktail { Name = "bacardi_special", AllowStudentDiscount = false, MaxOrderNumber = 2,
                    Ingredients = new List<(decimal amount, Ingredient ingredient)>
                    {
                        (0.5m, ingredients.FirstOrDefault(x => x.Name.Equals("gin"))),
                        (1.0m, ingredients.FirstOrDefault(x => x.Name.Equals("white stuff"))),
                        (1.0m, ingredients.FirstOrDefault(x => x.Name.Equals("grenadine"))),
                        (1.0m, ingredients.FirstOrDefault(x => x.Name.Equals("juice")))
                    }}
            };
        }

        public IEnumerable<Beverage> GetAll()
        {
            return _beverages;
        }

        public Beverage FindByName(string name)
        {
            return _beverages.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

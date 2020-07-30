using System.Collections.Generic;
using System.Linq;

namespace Pub.Models
{
    public class Cocktail : Beverage
    {
        public List<(decimal amount, Ingredient ingredient)> Ingredients { get; set; }
        public override int Price(bool student) => CalculatePrice(student);

        private int CalculatePrice(bool student)
        {
            if (AllowStudentDiscount && student)
            {
                var currentPrice = Ingredients?.Sum(item => item.ingredient.CalculatePrice(item.amount)) ?? 0;
                return currentPrice - currentPrice / 10;
            }

            return Ingredients?.Sum(item => item.ingredient.CalculatePrice(item.amount)) ?? 0;
        }
    }
}

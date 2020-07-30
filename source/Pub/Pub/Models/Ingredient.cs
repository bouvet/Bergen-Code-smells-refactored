namespace Pub.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }

        public int CalculatePrice(decimal amount)
        {
            var price = UnitPrice * amount;
            return (int) price;
        }
    }
}

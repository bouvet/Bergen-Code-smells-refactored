namespace Pub
{
    public class PubPrice
    {
        public const string OneBeer = "hansa";
        public const string OneCider = "grans";
        public const string AProperCider = "strongbow";
        public const string Gt = "gt";
        public const string BacardiSpecial = "bacardi_special";

        public int ComputeCost(string drink, bool student, int amount)
        {
            if (amount > 2 && (drink == "gt" || drink == "bacardi_special"))
            {
                throw new PubOrderException("Maximum number of drinks to order is 2.");
            }

            int price;

            if (drink.ToLower().Equals(OneBeer))
                price = 74;

            else if (drink.ToLower().Equals(OneCider))
                price = 103;

            else if (drink.ToLower().Equals(AProperCider))
                price = 110;

            else if (drink.ToLower().Equals(Gt))
                price = Ingredient6() + Ingredient5() + Ingredient4();

            else if (drink.ToLower().Equals(BacardiSpecial))
                price = Ingredient6() / 2 + Ingredient1() + Ingredient2() + Ingredient3();

            else
                throw new PubOrderException($"You can't order {drink}.");

            if (student && (drink == OneCider || drink == OneBeer || drink == AProperCider))
                price -= price / 10;

            return price * amount;
        }

        public (string drink, bool student, int amount) ParseOrder(string order)
        {
            if (string.IsNullOrEmpty(order))
                throw new PubOrderException("The order could not be empty and the format has to be correct.");

            var orderList = order.Split(' ');
            if (orderList == null || orderList.Length != 3)
                throw new PubOrderException("The format of your order  have to be correct. The order should have the format: [drink] [student] [amount].");

            var drink = orderList[0]?.Trim();
            if (!bool.TryParse(orderList[1], out var student))
                throw new PubOrderException("The student value should be [true/false].");

            if (!int.TryParse(orderList[2], out var amount))
                throw new PubOrderException("The amount value should be an integer.");

            if (amount <= 0)
                throw new PubOrderException("The amount value should be greater than 0.");

            return (drink, student, amount);
        }

        private static int Ingredient1()
        {
            return 65;
        }

        //one unit of grenadine
        private static int Ingredient2()
        {
            return 10;
        }

        //one unit of lime juice
        private static int Ingredient3()
        {
            return 10;
        }

        //one unit of green stuff
        private static int Ingredient4()
        {
            return 10;
        }

        //one unit of tonic water
        private static int Ingredient5()
        {
            return 20;
        }

        //one unit of gin
        private static int Ingredient6()
        {
            return 85;
        }
    }
}

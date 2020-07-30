using Pub.Services.Contracts;

namespace Pub
{
    public class PubPrice : IPubPrice
    {
        private readonly IBeverageService _beverageService;

        public PubPrice(IBeverageService beverageService)
        {
            _beverageService = beverageService;
        }

        public int ComputeCost(string drink, bool student, int amount)
        {
            var currentBeverage = _beverageService.GetByName(drink);
            if(currentBeverage == null)
                throw new PubOrderException($"Drink with name {drink} does not exist.");

            if (currentBeverage.MaxOrderNumber != -1 && amount > currentBeverage.MaxOrderNumber)
                throw new PubOrderException("Maximum number of drinks to order is 2.");

            return currentBeverage.Price(student) * amount;
        }

        public (string drink, bool student, int amount) ParseOrder(string order)
        {
            if (string.IsNullOrEmpty(order))
                throw new PubOrderException("The order could not be empty and the format has to be correct.");

            var orderList = order.Split(' ');
            if (orderList == null || orderList.Length != 3)
                throw new PubOrderException("The format of your order have to be correct. The order should have the format: [drink] [student] [amount].");

            var drink = orderList[0]?.Trim();
            if (!bool.TryParse(orderList[1], out var student))
                throw new PubOrderException("The student value should be [true/false].");

            if (!int.TryParse(orderList[2], out var amount))
                throw new PubOrderException("The amount value should be an integer.");

            if (amount <= 0)
                throw new PubOrderException("The amount value should be greater than 0.");

            return (drink, student, amount);
        }

    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pub;
using Pub.Repositories;
using Pub.Repositories.Contracts;
using Pub.Services;
using Pub.Services.Contracts;

namespace Tests
{
    [TestClass]
    public class PubTests
    {
        public IPubPrice PubPrice;

        public PubTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IBeverageRepository, BeverageHardCodedRepository>()
                .AddSingleton<IBeverageService, BeverageService>()
                .AddSingleton<IPubPrice, PubPrice>()
                .BuildServiceProvider();
            PubPrice = serviceProvider.GetService<IPubPrice>();
        }

        [TestMethod]
        public void OneBeerTest()
        {
            var actualPrice = PubPrice.ComputeCost("hansa", false, 1);
            Assert.AreEqual(74, actualPrice);
        }

        [TestMethod]
        public void OneBeerTest2()
        {
            var actualPrice = PubPrice.ComputeCost("hansa", true, 1);
            Assert.AreEqual(67, actualPrice);
        }

        [TestMethod]
        public void CidersAreCostly()
        {
            var actualPrice = PubPrice.ComputeCost("grans", false, 1);
            Assert.AreEqual(103, actualPrice);
        }

        [TestMethod]
        public void ProperCidersAreEvenMoreExpensive()
        {
            var actualPrice = PubPrice.ComputeCost("strongbow", false, 1);
            Assert.AreEqual(110, actualPrice);
        }

        [TestMethod]
        public void CoctailTest()
        {
            var actualPrice = PubPrice.ComputeCost("gt", false, 1);
            Assert.AreEqual(115, actualPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "Maximum number of drinks to order is 2.")]
        public void MaximumNumberOfDrinks()
        {
            _ = PubPrice.ComputeCost("gt", false, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "You can't order hhhh")]
        public void DrinkNotInMenu()
        {
            _ = PubPrice.ComputeCost("hhhh", false, 1);
        }
        
        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "The order could not be empty and the format has to be correct.")]
        public void OrderLineNullOrEmpty()
        {
            _ = PubPrice.ParseOrder(string.Empty);
            _ = PubPrice.ParseOrder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "The order could not be empty and the format has to be correct.")]
        public void OrderLineNotFormattedCorrect()
        {
            _ = PubPrice.ParseOrder(" ");
            _ = PubPrice.ParseOrder("hansa 12");
            _ = PubPrice.ParseOrder("hansa false");
            _ = PubPrice.ParseOrder("false 1");
        }

        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "The student value should be [true/false].")]
        public void StudentIsDefined()
        {
            _ = PubPrice.ParseOrder("hansa hhh 1");
        }
        
        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "The amount value should be an integer.")]
        public void AmountIsAnInteger()
        {
            _ = PubPrice.ParseOrder("hansa true 1.0");
            _ = PubPrice.ParseOrder("hansa true aaa");
        }

        [TestMethod]
        [ExpectedException(typeof(PubOrderException), "The amount value should be an integer.")]
        public void AmountIsGreaterThanZero()
        {
            _ = PubPrice.ParseOrder("hansa true 0");
            _ = PubPrice.ParseOrder("hansa true -1");
        }

        [TestMethod]
        public void DrinksStudent()
        {
            var actualPriceGt = PubPrice.ComputeCost("gt", true, 1);
            var actualPriceBs = PubPrice.ComputeCost("bacardi_special", true, 1);
            Assert.AreEqual(115, actualPriceGt);
            Assert.AreEqual(127, actualPriceBs);
        }
    }
}

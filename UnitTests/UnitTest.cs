using Newtonsoft.Json;
using NUnit.Framework;
using PriceCalculator;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    /// <summary>
    /// Unit Test to check Price Calculator 
    /// </summary>
    [TestFixture]
    public class UnitTest
    {
        List<Item> TestData;
        CalculatePrice calculateAmount;

        /// <summary>
        /// Setup up test data
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var myJsonString = File.ReadAllText("Items.json");
            TestData = JsonConvert.DeserializeObject<List<Item>>(myJsonString);
            calculateAmount = new CalculatePrice();
        }

        /// <summary>
        /// Test to check Price Calculator function
        /// </summary>
        /// <param name="quantities"></param>
        /// <param name="Subtotoal"></param>
        /// <param name="Totaldisocunt"></param>
        /// <param name="TotalPrice"></param>
        [TestCase(new int[] { 1, 2, 3, 4 }, 10.15, 0.4, 9.75)]
        [TestCase(new int[] { 1, 1, 1, 0 }, 2.75, 0, 2.75)]
        [TestCase(new int[] { 2, 2, 2, 2 }, 7.5, 1, 6.5)]
        public void PriceCalculatorTest(int[] quantities, double Subtotoal, double Totaldisocunt, double TotalPrice)
        {
            for (int i = 0; i <= 3; i++)
            {
                TestData[i].Quantiity = quantities[i];
            }
            PriceCalculation totoalAmount = calculateAmount.CalculateTotalPrice(TestData);
            Assert.NotNull(totoalAmount);
            Assert.AreEqual(Subtotoal, totoalAmount.Subtotoal);
            Assert.AreEqual(Totaldisocunt, totoalAmount.TotalDiscount);
            Assert.AreEqual(TotalPrice, totoalAmount.Subtotoal - totoalAmount.TotalDiscount);
        }
    }
}
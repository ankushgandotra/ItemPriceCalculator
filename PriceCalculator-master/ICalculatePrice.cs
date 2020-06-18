using System.Collections.Generic;

namespace PriceCalculator
{
  public interface ICalculatePrice
    {
        PriceCalculation CalculateTotalPrice(List<Item> items);
    }
}

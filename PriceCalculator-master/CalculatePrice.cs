using System;
using System.Collections.Generic;

namespace PriceCalculator
{
   public class CalculatePrice : ICalculatePrice
    {   
        /// <summary>
        /// Function to calculate price of input items.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
         public PriceCalculation CalculateTotalPrice(List<Item> items)
        {
            PriceCalculation price = new PriceCalculation();
            price.ItemDiscount = new List<ItemDiscount>();
            try
            {
                // calculate direct percentage disocunt
                foreach (Item item in items)
                {
                    //calculate price of each item 
                    double itemprice;
                    double discount = 0;
                    if (String.IsNullOrEmpty(item.DiscountPercentage.ToString()))
                    {
                        if (item.PriceUnit == Cuurency.p.ToString())
                        {
                            itemprice = ((double)((Convert.ToInt32(item.Quantiity) * Convert.ToInt32(item.Price))) / 100);
                        }
                        else
                        {
                            itemprice = (Convert.ToInt32(item.Quantiity) * Convert.ToDouble(item.Price));
                        }
                    }

                    else
                    {
                        if (item.PriceUnit == Cuurency.p.ToString())
                        {
                            itemprice = ((double)(Convert.ToInt32(item.Quantiity) * Convert.ToInt32(item.Price)) / 100);
                            discount = (itemprice * (Convert.ToInt32(item.DiscountPercentage))) / 100;
                        }
                        else
                        {
                            itemprice = (Convert.ToInt32(item.Quantiity) * Convert.ToDouble(item.Price));
                            discount = (itemprice * (Convert.ToInt32(item.DiscountPercentage))) / 100;
                        }
                    }
                    price.ItemDiscount.Add(new ItemDiscount
                    {
                        ItemCode = item.ItemCode,
                        Description = item.Description,
                        DiscountPercentage = item.DiscountPercentage,
                        DiscountAmount = discount,
                        ItemPrice = itemprice
                    });
                    price.Subtotoal = price.Subtotoal + itemprice;
                    price.TotalDiscount = price.TotalDiscount + discount;
                }

                // calculate for extended discount items
                // example: Buy 2 cans of Bean and get a loaf of bread for half price
                // This will be applied if there is no original discount on second item such as bread
                foreach (Item item in items)
                {
                    foreach (FurtherDiscount furtherDiscount in item.FurtherDiscount)
                    {
                        if (!String.IsNullOrEmpty(furtherDiscount.OnItemPurchased))
                        {
                            if (Convert.ToInt32(furtherDiscount.OnItemPurchased) > 0 && Convert.ToInt32(furtherDiscount.OnItemPurchased) <= item.Quantiity)
                            {
                                string discountedItem = furtherDiscount.DiscountedItemCode;
                                // apply extended discount only if there is no orginal discount on item
                                if ((String.IsNullOrEmpty(items.Find(x => x.ItemCode == discountedItem).DiscountPercentage)))
                                {
                                    // update discount details for item
                                    price.ItemDiscount.Find(x => x.ItemCode == discountedItem).DiscountPercentage = furtherDiscount.DiscountPercentage;

                                    price.ItemDiscount.Find(x => x.ItemCode == discountedItem).DiscountAmount =
                                    ((double)price.ItemDiscount.Find(x => x.ItemCode == discountedItem).ItemPrice) *
                                    (Convert.ToInt32(furtherDiscount.DiscountPercentage)) / 100;

                                    // update total discount 
                                    price.TotalDiscount = price.TotalDiscount + ((double)price.ItemDiscount.Find(x => x.ItemCode == discountedItem).ItemPrice) *
                                        (100 - Convert.ToInt32(furtherDiscount.DiscountPercentage)) / 100;

                                }
                            }
                        }
                    }
                }

                return price;

            }
            catch (Exception ex)
            {
                // write exception to logger and throw exception
                return price;
            }
        }

    }
}

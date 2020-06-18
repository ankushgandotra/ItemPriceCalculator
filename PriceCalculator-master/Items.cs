using System.Collections.Generic;

namespace PriceCalculator
{
   
  public class Item
    {        
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string PriceUnit { get; set; }
        public string Size { get; set; }
        public string DiscountPercentage { get; set; }
        public int Quantiity { get; set; }
        public List <FurtherDiscount> FurtherDiscount { get; set; }
    }

    public class FurtherDiscount
    {
        public string OnItemPurchased { get; set; }
        public string DiscountedItemCode { get; set; }
        public string DiscountPercentage { get; set; }      
    }

    public class PriceCalculation
    {
        public double Subtotoal { get; set; }
        public double Total { get; set; }
        public double TotalDiscount { get; set; }
        public List<ItemDiscount> ItemDiscount { get; set; }
    }

    public class ItemDiscount
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string DiscountPercentage { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPrice { get; set; }
    }

    public enum Cuurency
    {
        p  // pence        
    }
}

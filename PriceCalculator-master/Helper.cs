namespace PriceCalculator
{
   public static class Helper
    {
     /// <summary>
     /// Function to check if all input quantity values are integer or not
     /// </summary>
     /// <param name="quantities"></param>
     /// <returns></returns>
        public static bool CheckInteger(string[] quantities)
        {
            foreach (string quantitiy in quantities)
            {
                int integer;
                if (int.TryParse(quantitiy, out integer))
                {                    
                }
                else 
                {
                    return false;
                }
            }
            return true;
        }
    }
}

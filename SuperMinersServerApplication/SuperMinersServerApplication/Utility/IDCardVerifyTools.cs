using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperMinersServerApplication.Utility
{
    public class IDCardVerifyTools
    {
        private static readonly int[] IDCARDVERIFYCODE = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        private static readonly string[] IDCARDLASTTEXT = new string[] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };

        public static bool VerifyIDCard(string IDCardText)
        {
            if (string.IsNullOrEmpty(IDCardText))
            {
                return false;
            }
            bool matchValue = Regex.IsMatch(IDCardText, @"^([1-9][0-9]*X{0,1})$");
            if (!matchValue)
            {
                return false;
            }
            if (IDCardText.Length != 18)
            {
                return false;
            }
            string yearText = IDCardText.Substring(6, 4);
            string monthText = IDCardText.Substring(10, 2);
            string dayText = IDCardText.Substring(12, 2);
            int year = Convert.ToInt32(yearText);
            int month = Convert.ToInt32(monthText);
            int day = Convert.ToInt32(dayText);
            if (year < 1950 || year > DateTime.Now.Year - 16)
            {
                return false;
            }
            if (month > 12)
            {
                return false;
            }
            if (day > 31)
            {
                return false;
            }

            int sumResult = 0;
            for (int i = 0; i < IDCardText.Length - 1; i++)
            {
                int cardValue = Convert.ToInt32(IDCardText.Substring(i, 1));
                int plusValue = cardValue * IDCARDVERIFYCODE[i];
                sumResult += plusValue;
            }
            int plurValue = (sumResult) % 11;
            if (IDCardText.Substring(17, 1) != IDCARDLASTTEXT[plurValue])
            {
                return false;
            }

            return true;
        }
    }
}

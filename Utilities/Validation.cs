using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class Validation
    {

        public static void Validate(Object obj)
        {
            ValidationContext vc = new ValidationContext(obj);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, vc, results, true);
            if (!isValid)
            {
                string error = "";
                foreach (var item in results)
                {
                    error += item.ErrorMessage;
                    error += "\n";
                }
                throw new ArgumentException(error);
            }
        }

        public static void ValidDayMonth(int day, int month)
        {
            if (month is 1 or 3 or 5 or 7 or 8 or 10 or 12)
            {
                if (day is < 1 or > 31)
                {
                    throw new ArgumentException("This month " + month + " have 1 to 31 day");
                }
            }
            else if (month is 4 or 6 or 9 or 11)
            {
                if (day is < 1 or > 30)
                {
                    throw new ArgumentException("This month " + month + "have 1 to 30 day");
                }
            }
            else if (month == 2)
            {
                if (day is < 1 or > 29)
                {
                    throw new ArgumentException("This month " + month + " have 1 to 28 or 29 day");
                }       
            }
            else
            {
                throw new ArgumentException("Month from 1 to 12");
            }
        }

        public static void ValidNumberThanZero(int number, string message)
        {
            if(number < 1) { throw new ArgumentException(message); }
        }
    }
}

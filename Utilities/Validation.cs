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


    }
}

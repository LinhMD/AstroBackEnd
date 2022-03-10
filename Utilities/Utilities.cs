using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class Utilities
    {
        public static bool CheckTag(string tags, string tagsToCompare)
        {
            tags = tags.ToLower();
            tagsToCompare = tagsToCompare.ToLower();

            string[] ts = tags.Split("-");
            
            foreach (var tag in ts)
            {
                if (tagsToCompare.Contains(tag))
                    return true;
            }
            return false;
        }
    }
}

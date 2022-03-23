using AstroBackEnd.Models;
using AstroBackEnd.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AstroBackEnd.Utilities
{
    public class Utilities
    {
        public static bool CheckTag(string tags, string tagsToCompare)
        {
            if (tags == null || tagsToCompare == null) return false;

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
        Expression<Func<Aspect, AspectView>> CreateNewStatement(string fields)
        {
            // input parameter "o"
            ParameterExpression xParameter = Expression.Parameter(typeof(Aspect), "o");

            // new statement "new Data()"1
            NewExpression xNew = Expression.New(typeof(AspectView));

            // create initializers
            IEnumerable<MemberAssignment> bindings = fields.Split(',').Select(o => o.Trim())
                .Select(o => {

                    // property "Field1"
                    var fieldOfxNew = typeof(Aspect).GetProperty(o);

                    // original value "o.Field1"
                    var xOriginal = Expression.Property(xParameter, fieldOfxNew);

                    // set value "Field1 = o.Field1"
                    return Expression.Bind(fieldOfxNew, xOriginal);
                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<Aspect, AspectView>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda;
        }
    }

    

}

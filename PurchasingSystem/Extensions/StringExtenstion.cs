using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurchasingSystem.Extensions
{
    public static class StringExtenstion
    {
        public static Guid ToGuid(this string guidtext)
        {
            if (Guid.TryParse(guidtext, out Guid tempGuid))
            {
                return tempGuid;
            }
            else
            {
                //return null;
                return Guid.Empty;
            }
        }

    }
}
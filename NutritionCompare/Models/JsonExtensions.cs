using System;
using Newtonsoft.Json.Linq;

namespace NutritionCompare.Models
{
    public static class JsonExtensions
    {
        public static decimal? SelectTokenSafe(this JToken token, string key)
        {
            if (token.SelectToken(key) == null)
                return null;

            decimal d;
            if (Decimal.TryParse(token.SelectToken(key).ToString(), out d))
                return d;

            return null;
        }
    }
}
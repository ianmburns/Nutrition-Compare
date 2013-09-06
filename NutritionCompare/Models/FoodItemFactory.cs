using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NutritionCompare.Models
{
    public class FoodItemFactory
    {
        public static FoodItem FromJson(string json)
        {
            var jObject = JObject.Parse(json);
            var jFood = jObject["food"];

            var item = new FoodItem
            {
                Id = (int)jFood.SelectToken("food_id"),
                FoodName = (string)jFood.SelectToken("food_name"),
                Url = (string)jFood.SelectToken("food_url")
            };

            JToken jServings = null;
            //Try to find if jServings has more than one serving, or a single serving
            if (jFood["servings"]["serving"].Type == JTokenType.Array)
                jServings = jFood["servings"]["serving"];
            else if (jFood["servings"]["serving"].Type == JTokenType.Object)
            {
                jServings = jFood["servings"];
            }

            //Try to find 100g serving
            JToken jServing = null;
            var servingPos = 0;
            if (jServings != null)
            {
                if (jServings.Type == JTokenType.Array)
                {

                    foreach (var serving in jServings)
                    {
                        if (Find100GramServing(serving, ref jServing))
                            break;

                        servingPos++;
                    }
                }
                else
                {
                    Find100GramServing(jServings["serving"], ref jServing);
                }
            }

            SetServing(item.PerGram, jServing);

            //If first serving is not per gram, use that as "serving"
            if (jServings != null)
            {
                if (jServings.Type == JTokenType.Array)
                {
                    if (jServing == null || servingPos > 0)
                        jServing = jServings.FirstOrDefault();
                    else if (servingPos == 0 && jServings.Count() > 1)
                        jServing = jServings[1];
                }
                else
                {

                    jServing = jServings["serving"];
                }
            }

            SetServing(item.PerServing, jServing);

            return item;
        }

        private static bool Find100GramServing(JToken serving, ref JToken jServing)
        {
            decimal amount;

            if (serving.SelectToken("metric_serving_amount") != null &&
                decimal.TryParse(serving.SelectToken("metric_serving_amount").ToString(), out amount))
            {
                var unit = serving.SelectToken("metric_serving_unit").ToString();
                if (amount.Equals(100) && unit.Equals("g"))
                {
                    jServing = serving;
                    return true;
                }
            }
            return false;
        }

        private static void SetServing(Nutrition serving, JToken servingToken)
        {
            if (servingToken == null)
            {
                serving.ServingSize = "No Data Found";
                return;
            }

            serving.ServingSize = servingToken.SelectToken("serving_description").ToString();
            serving.Calories = servingToken.SelectTokenSafe("calories");
            serving.Carbohydrate = servingToken.SelectTokenSafe("carbohydrate");
            serving.Protein = servingToken.SelectTokenSafe("protein");
            serving.Fat = servingToken.SelectTokenSafe("fat");
            serving.SaturatedFat = servingToken.SelectTokenSafe("saturated_fat");
            serving.PolyunsaturatedFat = servingToken.SelectTokenSafe("polyunsaturated_fat");
            serving.MonounsaturatedFat = servingToken.SelectTokenSafe("monounsaturated_fat");
            serving.Cholesterol = servingToken.SelectTokenSafe("cholesterol");
            serving.Sodium = servingToken.SelectTokenSafe("sodium");
            serving.Potassium = servingToken.SelectTokenSafe("potassium");
            serving.Fiber = servingToken.SelectTokenSafe("fiber");
            serving.Sugar = servingToken.SelectTokenSafe("sugar");
            serving.VitaminA = servingToken.SelectTokenSafe("vitamin_a");
            serving.VitaminC = servingToken.SelectTokenSafe("vitamin_c");
            serving.Calcium = servingToken.SelectTokenSafe("calcium");
            serving.Iron = servingToken.SelectTokenSafe("iron");
        }

        public static IEnumerable<FoodItem> ListFromJson(string json)
        {
            var jObject = JObject.Parse(json);
            var jFoods = jObject["foods"];

            return jFoods["food"].Select(jFood => new FoodItem
            {
                Id = (int)jFood.SelectToken("food_id"),
                FoodName = (string)jFood.SelectToken("food_name"),
                Url = (string)jFood.SelectToken("food_url")
            });
        }
    }
}
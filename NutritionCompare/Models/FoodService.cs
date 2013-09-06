using System.Collections.Generic;
using System.Configuration;
using FatSecretSharp.Services;
using FatSecretSharp.Services.Requests;
using FatSecretSharp.Services.Responses;

namespace NutritionCompare.Models
{
    public class FoodService
    {
        private readonly string _consumerKey = ConfigurationManager.AppSettings["API_KEY"];
        private readonly string _consumerSecret = ConfigurationManager.AppSettings["API_SECRET"];

        public MultiFoodServingsDetails GetFood(int id)
        {
            var fooditem = new FoodDetails(_consumerKey, _consumerSecret);
            var itemrequest = new FoodDetailsRequest { FoodId = id };
            var response = fooditem.GetResponseSynchronously(itemrequest);

            if (response != null && response.HasResponse)
            {
                return response.food;
            }
            return null;
        }

        public List<FoodInfo> SearchFoods(string search)
        {
            var foodSearch = new FoodSearch(_consumerKey, _consumerSecret);
            var itemrequest = new FoodSearchRequest { SearchExpression = search };
            var response = foodSearch.GetResponseSynchronously(itemrequest);


            if (response != null && response.HasResults)
            {
                return response.foods.food;

            }
            return null;
        }
    }
}
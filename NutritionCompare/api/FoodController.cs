using System.Net;
using System.Net.Http;
using System.Web.Http;
using NutritionCompare.Models;

namespace NutritionCompare.api
{
    public class FoodController : ApiController
    {
        private readonly FoodService _service;

        public FoodController()
        {
            _service = new FoodService();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(string search)
        {
            var foods = _service.SearchFoods(search);
            return Request.CreateResponse(HttpStatusCode.OK, foods);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var food = _service.GetFood(id);
            return Request.CreateResponse(HttpStatusCode.OK, food);
        }
    }
}
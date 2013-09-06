namespace NutritionCompare.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string Url { get; set; }

        private readonly Nutrition _perGramServing = new Nutrition(ServingTypes.PerGram);
        public Nutrition PerGram
        {
            get { return _perGramServing; }
        }

        private readonly Nutrition _perServingServing = new Nutrition(ServingTypes.PerServing);
        public Nutrition PerServing
        {
            get { return _perServingServing; }
        }
    }
}
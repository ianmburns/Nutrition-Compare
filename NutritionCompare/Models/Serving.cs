namespace NutritionCompare.Models
{
    public class Nutrition
    {
        public Nutrition(ServingTypes servingType)
        {
            this.ServingType = servingType;
        }

        public ServingTypes ServingType { get; private set; }
    
        public string ServingSize { get; set; }

        public decimal? Calories { get; set; }
        public decimal? Carbohydrate { get; set; }
        public decimal? Protein { get; set; }
        public decimal? Fat { get; set; }
        public decimal? SaturatedFat { get; set; }
        public decimal? PolyunsaturatedFat { get; set; }
        public decimal? MonounsaturatedFat { get; set; }
        public decimal? Cholesterol { get; set; }
        public decimal? Sodium { get; set; }
        public decimal? Potassium { get; set; }
        public decimal? Fiber { get; set; }
        public decimal? Sugar { get; set; }
        public decimal? VitaminA { get; set; }
        public decimal? VitaminC { get; set; }
        public decimal? Calcium { get; set; }
        public decimal? Iron { get; set; }

    }

    public enum ServingTypes
    {
        PerServing = 1,
        PerGram = 2
    }
}
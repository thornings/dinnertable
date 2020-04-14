using System.Collections.Generic;

namespace ClientMadbordet.ViewModels
{
    public class MealWithFoodItemsViewModel<T, K>
    {
        public K Key;
        public IEnumerable<T> Values;
        public int Id;
        public int TotalCarbs;
        public int TotalProteins;
        public int TotalFats;
    }
}
using System.Collections.Generic;

namespace ClientMadbordet.ViewModels
{
    public class MealWithFoodItemsViewModel<T, K>
    {
        public K Key;
        public IEnumerable<T> Values;
        public int Id;

    }
}
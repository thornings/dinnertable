using ClientMadbordet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Repositories
{
    public interface ICalendarRepository
    {
        IEnumerable<Food> GetAllFood();
        IEnumerable<Meal> GetAllDaysMeals();
    }
}

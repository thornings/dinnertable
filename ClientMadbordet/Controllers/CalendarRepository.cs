using ClientMadbordet.Models;
using ClientMadbordet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Controllers
{
    public class CalendarRepository : ICalendarRepository
    {
        public IEnumerable<Meal> GetAllDaysMeals()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Food> GetAllFood()
        {
            throw new NotImplementedException();
        }
    }
}

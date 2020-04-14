using ClientMadbordet.Models;

namespace ClientMadbordet.Controllers
{
    public class CalendarMealViewModel
    {
        public Meal Meal { get; set; }
        public int TotalCarbs { get; set; }
        public int TotalProteins { get; set; }
        public int TotalFats { get; set; }
    }
}
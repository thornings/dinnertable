using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Models
{
    public class SearchModel
    {
        public string SearchValue { get; set; }
        public string FormattedDate{ get; set; }
        public int MealId { get; set; }
    }
}

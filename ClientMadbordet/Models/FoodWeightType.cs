using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Models
{
    public class FoodWeightType
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int WeightTypeId { get; set; }
        public WeightType WeightType { get; set; }

        [Required]
        public int Weight { get; set; }
    }
}

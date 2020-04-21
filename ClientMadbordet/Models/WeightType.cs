using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Models
{
    public class WeightType
    {
        public WeightType()
        { }

        [Key]
        public int WTID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UnitName { get; set; }


        public IList<FoodWeightType> FoodWeightTypes { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactTestTask.Models
{
    public class DrinkAJAXRequest
    {
        public int drinkId;
        public string drinkName;
        public int drinkAmount;
		public ushort drinkCost;
        public bool isDrinkAvailable;
        public string img;
    }
}

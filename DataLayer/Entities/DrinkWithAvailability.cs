using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class DrinkWithAvailability
    {
        public DrinkWithAvailability(Drink drink, DrinkAvailability drinkAvailability)
        {
            Drink = drink;
            DrinkAvailability = drinkAvailability;
        }

        public Drink Drink { get; private set; }
        public DrinkAvailability DrinkAvailability { get; private set; }
    }
}

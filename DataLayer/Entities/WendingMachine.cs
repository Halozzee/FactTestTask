using DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class WendingMachine
    {
        public int CurrentBalance;

        public WendingMachine() 
        {

        }

        public bool TryBuyDrink(int drinkId) 
        {
            if (DrinkDeliveryMaster.IsDrinkAvailable(drinkId))
            {
                Drink drink = DrinkDeliveryMaster.GetDrinkById(drinkId);

                if (drink.Cost <= CurrentBalance)
                {
                    CurrentBalance -= drink.Cost;
                    DrinkDeliveryMaster.ChangeDrinkAmount(drinkId, DrinkAmountChangeAction.Decrement);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}

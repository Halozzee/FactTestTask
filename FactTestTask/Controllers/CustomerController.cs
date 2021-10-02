using DataLayer.Database;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactTestTask.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetBuyDrinkMenu() 
        {
            List<DrinkWithAvailability> drinkAvsList = DrinkDeliveryMaster.GetAllDrinkWithAvailabilityList();
            return PartialView(drinkAvsList);
        }

        public void AddToBalance(int toAdd) 
        {
            CustomerData.CurrentBalance += toAdd;
        }

        public void BuyDrink(int drinkId)
        {
            if (DrinkDeliveryMaster.IsDrinkAvailable(drinkId) && CustomerData.CurrentBalance >= DrinkDeliveryMaster.GetDrinkById(drinkId).Cost)
            {
                DrinkDeliveryMaster.ChangeDrinkAmount(drinkId, DrinkAmountChangeAction.Decrement);
            }
        }
    }
}

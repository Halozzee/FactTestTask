using DataLayer.Database;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using FactTestTask.Models;

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

        public string AddToBalance(int toAdd) 
        {
            CustomerData.CurrentBalance += toAdd;
            return CustomerData.CurrentBalance.ToString();
        }

        public string BuyDrink(int drinkId)
        {
            DrinkTransactionResult result = new DrinkTransactionResult();

            result.DrinkId = drinkId;
            result.OperationCode = OperationResult.None;
            result.NewBalance = CustomerData.CurrentBalance;

            if (!DrinkDeliveryMaster.IsDrinkAvailable(drinkId))
            {
                result.OperationCode = OperationResult.Unavailable;
                return JsonConvert.SerializeObject(result);
            }
            else if(CustomerData.CurrentBalance < DrinkDeliveryMaster.GetDrinkById(drinkId).Cost)
            {
                result.OperationCode = OperationResult.InsufficientFunds;
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                CustomerData.CurrentBalance -= DrinkDeliveryMaster.GetDrinkById(drinkId).Cost;
                result.OperationCode = OperationResult.Success;
                result.NewBalance = CustomerData.CurrentBalance;

                DrinkDeliveryMaster.ChangeDrinkAmount(drinkId, DrinkAmountChangeAction.Decrement);

                if(DrinkDeliveryMaster.GetDrinkWithAvailabilityByDrinkId(drinkId).DrinkAvailability.Amount == 0)
                {
                    result.OperationCode = OperationResult.LastItemBought;
                }

                return JsonConvert.SerializeObject(result);
            }
        }
    }
}
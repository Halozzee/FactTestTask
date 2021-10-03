using DataLayer.Database;
using DataLayer.Entities;
using FactTestTask.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FactTestTask.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index(string passwordString)
        {
            ViewBag.IsOneCoinAllowed    = CustomerData.IsOneCoinAllowed;
            ViewBag.IsTwoCoinAllowed    = CustomerData.IsTwoCoinAllowed;
            ViewBag.IsFiveCoinAllowed   = CustomerData.IsFiveCoinAllowed;
            ViewBag.IsTenCoinAllowed    = CustomerData.IsTenCoinAllowed;

            List<DrinkWithAvailability> drinkAvsList = DrinkDeliveryMaster.GetAllDrinkWithAvailabilityList();

            passwordString = "admin";

            if (passwordString == "admin")
                return View(drinkAvsList);
            else
                return View("NotAdmin");
        }

        [HttpPost]
        public void RemoveDrink(int drinkId) 
        {
            DrinkDeliveryMaster.RemoveDrink(drinkId);
        }

        [HttpPost]
        public IActionResult AddDrink(string drinkRequest)
        {
            DrinkAJAXRequest request = JsonConvert.DeserializeObject<DrinkAJAXRequest>(drinkRequest);
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = request.drinkName, Cost = request.drinkCost});
            int drinkBdId = DrinkDeliveryMaster.GetLastDrinkId();
            DrinkDeliveryMaster.ChangeDrinkAmount(drinkBdId, DrinkAmountChangeAction.SetAmount, request.drinkAmount);
            DrinkDeliveryMaster.ToggleDrinkAvailability(drinkBdId, request.isDrinkAvailable);
            DrinkWithAvailability drinkWithAv = DrinkDeliveryMaster.GetDrinkWithAvailabilityByDrinkId(drinkBdId);
            return PartialView("_addDrink", drinkWithAv);
        }

        [HttpPost]
        public void EditDrink(string drinkRequest)
        {
            DrinkAJAXRequest request = JsonConvert.DeserializeObject<DrinkAJAXRequest>(drinkRequest);
            DrinkWithAvailability toEdit = DrinkDeliveryMaster.GetDrinkWithAvailabilityByDrinkId(request.drinkId);

            toEdit.Drink.Name = request.drinkName;
            toEdit.Drink.Cost = request.drinkCost;
            toEdit.DrinkAvailability.Amount = request.drinkAmount;
            toEdit.DrinkAvailability.IsAvailable = request.isDrinkAvailable;

            DrinkDeliveryMaster.UpdateDrinkWithAvailability(toEdit);
        }

        [HttpPost]
        public string GetDrinkEditParams(int drinkId)
        {
            DrinkWithAvailability drinkWithAv = DrinkDeliveryMaster.GetDrinkWithAvailabilityByDrinkId(drinkId);
            DrinkAJAXRequest toSend = new DrinkAJAXRequest();

            toSend.drinkId = drinkWithAv.Drink.Id;
            toSend.drinkName = drinkWithAv.Drink.Name;
            toSend.drinkCost = drinkWithAv.Drink.Cost;
            toSend.isDrinkAvailable = drinkWithAv.DrinkAvailability.IsAvailable;
            toSend.drinkAmount = drinkWithAv.DrinkAvailability.Amount;

            return JsonConvert.SerializeObject(toSend);
        }

        [HttpPost]
        public void ToggleCoin(int coinValue, bool isCoinAvailable)
        {
            switch (coinValue)
            {
                case 1:
                    CustomerData.IsOneCoinAllowed = isCoinAvailable;
                    break;
                case 2:
                    CustomerData.IsTwoCoinAllowed = isCoinAvailable;
                    break;
                case 5:
                    CustomerData.IsFiveCoinAllowed = isCoinAvailable;
                    break;
                case 10:
                    CustomerData.IsTenCoinAllowed = isCoinAvailable;
                    break;
                default:
                    break;
            }
        }
    }
}

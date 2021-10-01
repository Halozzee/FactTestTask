using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public enum DrinkAmountChangeAction 
    {
        NotPickedExceptionThrower,
        SetAmount,
        Increment,
        Decrement
    }

    public static class DrinkDeliveryMaster
    {
        private static EFDBContext _context;

        public static bool IsSampleDataNeeded() 
        {
            return !_context.Drinks.Any();
        }

        public static void SetContext(EFDBContext context) 
        {
            _context = context;
        }

        public static Drink GetDrinkById(int drinkId) 
        {
            return _context.Drinks.ToList().Find(x => x.Id == drinkId);
        }

        public static bool IsDrinkAvailable(int drinkId) 
        {
            return _context.DrinkAmounts.ToList().Find(y => y.DrinkId == drinkId).IsAvailable;
        }

        public static List<Drink> GetAllDrinks() 
        {
            return _context.Drinks.ToList();
        }

        public static List<Drink> GetAvailableDrinks() 
        {
            List<DrinkAvailability> daList = _context.DrinkAmounts.ToList();
            return _context.Drinks.ToList().Where(x => daList.FindIndex(y => y.DrinkId == x.Id && y.IsAvailable) != -1).ToList();
        }

        public static void AddNewDrink(Drink drink)
        {
            _context.Drinks.Add(drink);
            _context.SaveChanges();
            var drinksList = _context.Drinks.ToList();
            _context.DrinkAmounts.Add(new DrinkAvailability { DrinkId = drinksList[drinksList.Count - 1].Id, Amount = 0 }); ;
            _context.SaveChanges();
        }

        public static void RemoveDrink(int drinkId)
        {
            var matchDrink = _context.Drinks.ToList().Find(x => x.Id == drinkId);
            _context.Drinks.Remove(matchDrink);
            _context.DrinkAmounts.Remove(_context.DrinkAmounts.ToList().Find(x => x.DrinkId == drinkId));
            _context.SaveChanges();
        }

        public static void ChangeDrinkAmount(int drinkId, DrinkAmountChangeAction changeAction, int amount = 0) 
        {
            switch (changeAction)
            {
                case DrinkAmountChangeAction.NotPickedExceptionThrower:
                    throw new ArgumentException("Не выбрано действие!");
                case DrinkAmountChangeAction.SetAmount:
                    _context.DrinkAmounts.ToList().Find(x => x.DrinkId == drinkId).Amount = amount;
                    CheckDrinkAvailabilityAfterMath(drinkId);
                    break;
                case DrinkAmountChangeAction.Increment:
                    _context.DrinkAmounts.ToList().Find(x => x.DrinkId == drinkId).Amount++;
                    break;
                case DrinkAmountChangeAction.Decrement:
                    _context.DrinkAmounts.ToList().Find(x => x.DrinkId == drinkId).Amount--;
                    CheckDrinkAvailabilityAfterMath(drinkId);
                    break;
                default:
                    break;
            }
            _context.SaveChanges();
        }

        private static void CheckDrinkAvailabilityAfterMath(int drinkId) 
        {
            var matchDrinkAv = _context.DrinkAmounts.ToList().Find(x => x.DrinkId == drinkId);

            if (matchDrinkAv.Amount <= 0)
            {
                matchDrinkAv.IsAvailable = false;
            }
        }

        public static void ToggleDrinkAvailability(int drinkId, bool isAvailable) 
        {
            var matchDrinkAv = _context.DrinkAmounts.ToList().Find(x => x.DrinkId == drinkId);

            if (matchDrinkAv.Amount > 0)
            {
                matchDrinkAv.IsAvailable = isAvailable;
                _context.SaveChanges();
            }
        }
    }
}

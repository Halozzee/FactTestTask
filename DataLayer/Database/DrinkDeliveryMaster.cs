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
        public static bool IsSampleDataNeeded() 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                return !_context.Drinks.Any();
            }
        }

        public static int GetLastDrinkId() 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                var drinksList = _context.Drinks.ToList();
                return drinksList[drinksList.Count - 1].Id;
            }
        }
        public static Drink GetDrinkById(int drinkId) 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                return _context.Drinks.ToList().Find(x => x.Id == drinkId);
            }
        }

        public static bool IsDrinkAvailable(int drinkId) 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                return _context.DrinkAvailabilities.ToList().Find(y => y.DrinkId == drinkId).IsAvailable;
            }
        }

        public static List<Drink> GetAllDrinks() 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                return _context.Drinks.ToList();
            }
        }

        public static List<DrinkWithAvailability> GetAllDrinkWithAvailabilityList() 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                var drinkList = _context.Drinks.ToList();
                var drinkAvList = _context.DrinkAvailabilities.ToList();

                List<DrinkWithAvailability> result = new List<DrinkWithAvailability>();

                for (int i = 0; i < drinkList.Count; i++)
                {
                    result.Add(new DrinkWithAvailability(drinkList[i], drinkAvList[i]));
                }

                return result;
            }
        }

        public static void UpdateDrinkWithAvailability(DrinkWithAvailability dav) 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                _context.Drinks.Update(dav.Drink);
                _context.DrinkAvailabilities.Update(dav.DrinkAvailability);
                _context.SaveChanges();
            }
        }

        public static DrinkWithAvailability GetDrinkWithAvailabilityByDrinkId(int DrinkId)
        {
            using (EFDBContext _context = new EFDBContext())
            {
                var drink = _context.Drinks.ToList().Find(x => x.Id == DrinkId);
                var drinkAv = _context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == DrinkId);

                DrinkWithAvailability result = new DrinkWithAvailability(drink, drinkAv);
                return result;
            }
        }

        public static List<Drink> GetAvailableDrinks() 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                List<DrinkAvailability> daList = _context.DrinkAvailabilities.ToList();
                return _context.Drinks.ToList().Where(x => daList.FindIndex(y => y.DrinkId == x.Id && y.IsAvailable) != -1).ToList();
            }
        }

        public static void AddNewDrink(Drink drink)
        {
            using (EFDBContext _context = new EFDBContext())
            {
                _context.Drinks.Add(drink);
                _context.SaveChanges();
                var drinksList = _context.Drinks.ToList();
                _context.DrinkAvailabilities.Add(new DrinkAvailability { DrinkId = drinksList[drinksList.Count - 1].Id, Amount = 1, IsAvailable = true });
                _context.SaveChanges();
            }
        }

        public static void RemoveDrink(int drinkId)
        {
            using (EFDBContext _context = new EFDBContext())
            {
                var matchDrink = _context.Drinks.ToList().Find(x => x.Id == drinkId);
                _context.Drinks.Remove(matchDrink);
                _context.DrinkAvailabilities.Remove(_context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == drinkId));
                _context.SaveChanges();
            }
        }

        public static void ChangeDrinkAmount(int drinkId, DrinkAmountChangeAction changeAction, int amount = 0) 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                switch (changeAction)
                {
                    case DrinkAmountChangeAction.NotPickedExceptionThrower:
                        throw new ArgumentException("Не выбрано действие!");
                    case DrinkAmountChangeAction.SetAmount:
                        _context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == drinkId).Amount = amount;
                        break;
                    case DrinkAmountChangeAction.Increment:
                        _context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == drinkId).Amount++;
                        break;
                    case DrinkAmountChangeAction.Decrement:
                        _context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == drinkId).Amount--;
                        break;
                    default:
                        break;
                }
                _context.SaveChanges();
                CheckDrinkAvailabilityAfterMath(drinkId);
            }
        }

        private static void CheckDrinkAvailabilityAfterMath(int drinkId) 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                var matchDrinkAv = _context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == drinkId);

                if (matchDrinkAv.Amount <= 0)
                {
                    matchDrinkAv.IsAvailable = false;
                }
                _context.SaveChanges();
            }
        }

        public static void ToggleDrinkAvailability(int drinkId, bool isAvailable) 
        {
            using (EFDBContext _context = new EFDBContext())
            {
                var matchDrinkAv = _context.DrinkAvailabilities.ToList().Find(x => x.DrinkId == drinkId);

                if (matchDrinkAv.Amount > 0)
                {
                    matchDrinkAv.IsAvailable = isAvailable;
                    _context.SaveChanges();
                }
            }
        }
    }
}

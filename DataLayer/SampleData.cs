using DataLayer.Database;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class SampleData
    {
        public static void InitData()
        {
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Jugger-nog",    Cost = 15 });
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Nuke-Cola",     Cost = 6 });
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Coca-Cola",     Cost = 4 });
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Dr. Grim",      Cost = 11 });
        }
    }
}

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
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Jugger-nog",    Cost = 15,  Img = @"https://i.redd.it/jjn706l8jq911.jpg" });
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Nuka-Cola",     Cost = 6,   Img = @"https://image.posterlounge.com/images/l/1886101.jpg" });
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Coca-Cola",     Cost = 4, Img = @"https://www.officemag.ru/goods/620870/49061b8b93af1bc7a9ff07b2892d49c1_xl.jpg" });
            DrinkDeliveryMaster.AddNewDrink(new Drink { Name = "Dr. Grim",      Cost = 11, Img = @"https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/7354c042209689.580d5641b3d73.png" });
        }
    }
}

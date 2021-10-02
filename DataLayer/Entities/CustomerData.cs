using DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public static class CustomerData
    {
        public static int CurrentBalance        = 0;

        public static bool IsOneCoinAllowed     = true;
        public static bool IsTwoCoinAllowed     = true;
        public static bool IsFiveCoinAllowed    = true;
        public static bool IsTenCoinAllowed     = true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class DrinkAvailability
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public int Amount { get; set; }
        public bool IsAvailable { get; set; }
    }
}

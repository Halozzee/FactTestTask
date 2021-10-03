using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ushort Cost { get; set; }
        public string Img { get; set; } = "img/dataobjects/def.png";
    }
}

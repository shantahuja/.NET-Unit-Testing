using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCST240
{
    public class Item : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal ItemTotal
        {
            get { return Quantity * UnitPrice; }
        }
    }
}

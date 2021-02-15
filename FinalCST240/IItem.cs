using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCST240
{
    public interface IItem
    {
        int Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        decimal UnitPrice { get; set; }
        decimal ItemTotal { get; }

    }
}

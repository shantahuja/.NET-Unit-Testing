using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCST240
{
    public interface IShoppingCart
    {
        int Id { get; set; }
        decimal SubTotal { get; set; }
        void AddItem(Item item);
        void DeleteItem(Item item);
        void UpdateItem(Item item);
        IList<Item> ReadFromFile();
        void SaveToFile(IList<Item> cart);
        IEnumerable<Item> FindAll();
        Item FindById(int id);
    }
}

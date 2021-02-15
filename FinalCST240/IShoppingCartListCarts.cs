using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCST240
{
    public interface IShoppingCartListCarts
    {
        void AddShoppingCart(ShoppingCart shoppingcart);
        void DeleteShoppingCart(ShoppingCart shoppingcart);
        void UpdateShoppingCart(ShoppingCart shoppingcart);
        IList<ShoppingCart> ReadFromFile();
        void SaveToFile(IList<ShoppingCart> cart);
        IEnumerable<ShoppingCart> FindAll();
        ShoppingCart FindById(int id);
    }
}

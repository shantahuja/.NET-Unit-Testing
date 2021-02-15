using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinalCST240
{
    public class ShoppingCartListCarts : IShoppingCartListCarts
    {
        //public const string ShoppingCartDb = @"../../ShoppingCartDb.json";
        //doesnt work for some reason
        public const string ShoppingCartDb = @"C:\Users\shant\source\repos\FinalCST240\FinalCST240\ShoppingCartDb.json";
        public void AddShoppingCart(ShoppingCart shoppingcart)
        {
            if (shoppingcart == null) throw new InvalidOperationException("newItem was null");
            var carts = ReadFromFile();
            var maxId = 0;
            if (carts == null)
            {
                maxId = 0;
                carts = new List<ShoppingCart>();
            }
            else
            {
                maxId = carts.Count == 0 ? 0 : carts.Max(a => a.Id);
            }

            shoppingcart.Id = maxId + 1;
            carts.Add(shoppingcart);

            SaveToFile(carts);
        }

        public void DeleteShoppingCart(ShoppingCart shoppingcart)
        {
            var carts = ReadFromFile();
            var deleteCart = carts.ToList().SingleOrDefault(a => a.Id.Equals(shoppingcart.Id));
            if (deleteCart == null) throw new InvalidOperationException("ShoppingCart not found");

            for (int i = 1; i < deleteCart.FindAll().Count(); i++)
            {
                if (deleteCart.FindById(i) != null)
                {
                    deleteCart.DeleteItem(deleteCart.FindById(i));
                }
            }
            carts.Remove(deleteCart);

            SaveToFile(carts);
        }

        public void UpdateShoppingCart(ShoppingCart shoppingcart)
        {
            var carts = ReadFromFile();
            var editItem = carts.ToList().SingleOrDefault(a => a.Id.Equals(shoppingcart.Id));
            if (editItem == null) throw new InvalidOperationException("Account not found");
            var index = carts.IndexOf(editItem);
            carts.Insert(index, shoppingcart);
            carts.Remove(editItem);
            SaveToFile(carts);
        }

        public IList<ShoppingCart> ReadFromFile()
        {
            if (!File.Exists(ShoppingCartDb))
            {
                throw new InvalidOperationException("File not found");
            }

            var json = File.ReadAllText(ShoppingCartDb);
            return JsonConvert.DeserializeObject<List<ShoppingCart>>(json);
        }

        public void SaveToFile(IList<ShoppingCart> cart)
        {
            var json = JsonConvert.SerializeObject(cart, Formatting.Indented);
            File.WriteAllText(ShoppingCartDb, json);
        }

        public IEnumerable<ShoppingCart> FindAll()
        {
            return ReadFromFile();
        }

        public ShoppingCart FindById(int id)
        {
            return FindAll().SingleOrDefault(shoppingcart => shoppingcart.Id.Equals(id));
        }
    }
}

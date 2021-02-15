using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinalCST240
{
    public class ShoppingCart : IShoppingCart
    {
        public ShoppingCart(decimal subTotal)
        {
            SubTotal = subTotal;
        }

        public ShoppingCart()
        {

        }

        //public const string ItemDb = "ItemDb.json";
        //doesn't work for some reason
        public const string ItemDb = @"C:\Users\shant\source\repos\FinalCST240\FinalCST240\ItemDb.json";
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public void AddItem(Item item)
        {
            if (item == null) throw new InvalidOperationException("newItem was null");
            var items = ReadFromFile();
            var maxId = 0;
            if (items == null)
            {
                maxId = 0;
                items = new List<Item>();
            }
            else
            {
                maxId = items.Count == 0 ? 0 : items.Max(a => a.Id);
            }

            item.Id = maxId + 1;
            items.Add(item);

            SubTotal = SubTotal + item.ItemTotal;

            SaveToFile(items);
        }

        public void DeleteItem(Item item)
        {
            var items = ReadFromFile();
            var deleteItem = items.ToList().SingleOrDefault(a => a.Id.Equals(item.Id));
            if (deleteItem == null) throw new InvalidOperationException("Item not found");

            items.Remove(deleteItem);
            SubTotal = SubTotal - item.ItemTotal;

            SaveToFile(items);
        }

        public void UpdateItem(Item item)
        {
            var items = ReadFromFile();
            var editItem = items.ToList().SingleOrDefault(a => a.Id.Equals(item.Id));
            if (editItem == null) throw new InvalidOperationException("Account not found");
            var index = items.IndexOf(editItem);
            items.Insert(index, item);
            SubTotal = SubTotal + item.ItemTotal;
            items.Remove(editItem);
            SubTotal = SubTotal - editItem.ItemTotal;
            SaveToFile(items);
        }

        public IList<Item> ReadFromFile()
        {
            if (!File.Exists(ItemDb))
            {
                throw new InvalidOperationException("File not found");
            }

            var json = File.ReadAllText(ItemDb);
            return JsonConvert.DeserializeObject<List<Item>>(json);
        }

        public void SaveToFile(IList<Item> cart)
        {
            var json = JsonConvert.SerializeObject(cart, Formatting.Indented);
            File.WriteAllText(ItemDb, json);
        }

        public IEnumerable<Item> FindAll()
        {
            return ReadFromFile();
        }

        public Item FindById(int id)
        {
            return FindAll().SingleOrDefault(item => item.Id.Equals(id));
        }
    }
}

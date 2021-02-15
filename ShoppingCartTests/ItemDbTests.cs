using System;
using System.Collections.Generic;
using Autofac;
using FinalCST240;
using FinalCST240.Startup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingCartTests
{
    [TestClass]
    public class ItemDbTests
    {
        private IList<Item> m_ItemDb;
        private Bootstrapper bootstrapper = new Bootstrapper();
        private IShoppingCart m_shopping_cart;
        private IList<Item> m_ItemJson;

        [TestInitialize()]
        public void Init()  
        {
            m_shopping_cart = bootstrapper.BootStrap().Resolve<IShoppingCart>();
            //doesnt work for some reason
            //m_shopping_cart = new ShoppingCart();

            m_ItemDb = new List<Item>()
            {
                new Item
                {
                    Id = 1,
                    Name = "banana",
                    Quantity = 3,
                    UnitPrice = 0.99m,
                },
                new Item
                {
                    Id = 2,
                    Name = "apple",
                    Quantity = 3,
                    UnitPrice = 0.99m,
                },
                new Item
                {
                    Id = 3,
                    Name = "pear",
                    Quantity = 3,
                    UnitPrice = 0.99m,
                },
                new Item
                {
                    Id = 4,
                    Name = "grape bundle",
                    Quantity = 3,
                    UnitPrice = 0.99m,
                }
            };

            m_shopping_cart.SaveToFile(m_ItemDb);

            m_ItemJson = m_shopping_cart.ReadFromFile();
        }

        [TestMethod]
        public void AddItemTest()
        {
            Item newItem = new Item
            {
                Id = 0,
                Name = "canned beans",
                Quantity = 3,
                UnitPrice = 1.50m
            };
            m_shopping_cart.AddItem(newItem);
           

            Assert.AreEqual("canned beans", m_shopping_cart.FindById(5).Name);
        }

        [TestMethod()]
        public void DeleteItemTest()
        {
            Item deleteItem = new Item
            {
                Id = 4,
                Name = "grape bundle",
                Quantity = 3,
                UnitPrice = 0.99m,
            };
            m_shopping_cart.DeleteItem(deleteItem);
        }

        [TestMethod()]
        public void UpdateItemTest()
        {
            Item updateItem = new Item
            {
                Id = 4,
                Name = "grape bundle",
                Quantity = 2,
                UnitPrice = 0.99m,
            };

            m_shopping_cart.UpdateItem(updateItem);

            Assert.AreEqual(2, m_shopping_cart.FindById(4).Quantity);
        }
    }
}

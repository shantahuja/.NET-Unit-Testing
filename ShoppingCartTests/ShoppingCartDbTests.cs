using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using FinalCST240;
using FinalCST240.Startup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingCartTests
{
    [TestClass]
    public class ShoppingCartDbTests
    {
        private IList<Item> m_ItemDb;
        private IList<Item> m_ItemDb2;
        private IList<Item> m_ItemDb3;
        private IList<ShoppingCart> m_ShoppingCartDb;
        private Bootstrapper bootstrapper = new Bootstrapper();
        private IShoppingCartListCarts m_ShoppingCartListCarts;
        private IList<ShoppingCart> m_ShoppingCartJson;

        [TestInitialize()]
        public void Init()
        {
            m_ShoppingCartListCarts = bootstrapper.BootStrap().Resolve<IShoppingCartListCarts>();
            //doesn't work for some reason
            //m_ShoppingCartListCarts = new ShoppingCartListCarts();
            
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
            m_ItemDb2 = new List<Item>()
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
            m_ItemDb3 = new List<Item>()
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

            m_ShoppingCartDb = new List<ShoppingCart>()
            {
                new ShoppingCart {Id = 1},
                new ShoppingCart {Id = 2},
                new ShoppingCart {Id = 3}

            };

            for (int i = 0; i < m_ItemDb.Count; i++)
            {
                m_ShoppingCartDb[0].AddItem(m_ItemDb[i]);
            }

            for (int i = 0; i < m_ItemDb.Count; i++)
            {
                m_ShoppingCartDb[1].AddItem(m_ItemDb2[i]);
            }

            for (int i = 0; i < m_ItemDb.Count; i++)
            {
                m_ShoppingCartDb[2].AddItem(m_ItemDb3[i]);
            }

            m_ShoppingCartListCarts.SaveToFile(m_ShoppingCartDb);

            m_ShoppingCartJson = m_ShoppingCartListCarts.ReadFromFile();
        }
        [TestMethod]
        public void AddShoppingCartTest()
        {
            ShoppingCart addShoppingCart = new ShoppingCart()
            {
                Id = 0
            };

            m_ShoppingCartListCarts.AddShoppingCart(addShoppingCart);
            Assert.AreEqual(4, m_ShoppingCartListCarts.FindAll().Count());
        }

        [TestMethod]
        public void DeleteShoppingCartsTest()
        {
            ShoppingCart deleteShoppingCart = new ShoppingCart()
            {
                Id = 1
            };

            m_ShoppingCartListCarts.DeleteShoppingCart(deleteShoppingCart);
            Assert.AreEqual(2, m_ShoppingCartListCarts.FindAll().Count());

        }

        [TestMethod]
        public void UpdateShoppingCartTest()
        {
            ShoppingCart updateShoppingCart = new ShoppingCart()
            {
                Id = 1
            };

            m_ShoppingCartListCarts.UpdateShoppingCart(updateShoppingCart);
            Assert.AreEqual(1, m_ShoppingCartListCarts.FindById(1).Id);
        }
    }
}

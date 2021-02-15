using System;
using System.Collections.Generic;
using System.Linq;
using FinalCST240;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ShoppingCartTests
{
    [TestClass]
    public class ShoppingCartMockTests
    {
        private IList<Item> m_ItemDb;
        private IList<Item> m_ItemDb2;
        private IList<Item> m_ItemDb3;
        private Mock<IShoppingCartListCarts> m_shopping_carts_mock;
        private IList<ShoppingCart> m_shopping_cartDb;
        [TestInitialize()]
        public void Init()
        {

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

            m_shopping_cartDb = new List<ShoppingCart>
            {
                new ShoppingCart {Id = 1},
                new ShoppingCart {Id = 2},
                new ShoppingCart {Id = 3}
            };

            for (int i = 0; i < m_ItemDb.Count; i++)
            {
                m_shopping_cartDb[0].AddItem(m_ItemDb[i]);
            }

            for (int i = 0; i < m_ItemDb.Count; i++)
            {
                m_shopping_cartDb[1].AddItem(m_ItemDb2[i]);
            }

            for (int i = 0; i < m_ItemDb.Count; i++)
            {
                m_shopping_cartDb[2].AddItem(m_ItemDb3[i]);
            }



            m_shopping_carts_mock = new Mock<IShoppingCartListCarts>();

            m_shopping_carts_mock.Setup(a => a.FindAll()).Returns(
                () =>
                {
                    var carts = new List<ShoppingCart>();

                    m_shopping_cartDb.ToList().ForEach(cart =>
                    {
                        carts.Add(new ShoppingCart()
                        {
                            Id = cart.Id,
                        });
                    });

                    return carts;
                });

            m_shopping_carts_mock.Setup(a => a.AddShoppingCart(It.IsAny<ShoppingCart>())).Callback(
                (ShoppingCart cart) =>
                {
                    if (cart == null) throw new InvalidOperationException("Add cart - cart was null");
                    if (cart.Id <= 0)
                    {
                        cart.Id = m_shopping_cartDb.Max(a => a.Id) + 1;
                        m_shopping_cartDb.Add(cart);
                    }

                });

            m_shopping_carts_mock.Setup(a => a.DeleteShoppingCart(It.IsAny<ShoppingCart>())).Callback(
                (ShoppingCart cart) =>
                {
                    var deleteAccount = m_shopping_cartDb.SingleOrDefault(a => a.Id.Equals(cart.Id));
                    m_shopping_cartDb.Remove(deleteAccount);
                });

            m_shopping_carts_mock.Setup(a => a.UpdateShoppingCart(It.IsAny<ShoppingCart>())).Callback(
                (ShoppingCart cart) =>
                {
                    if (cart == null) throw new InvalidOperationException("Edit ShoppingCart - cart was null");
                    var updateItem = m_shopping_cartDb.SingleOrDefault(a => a.Id.Equals(cart.Id));
                    if (updateItem == null) throw new InvalidOperationException("ShoppingCart was null");
                    updateItem.Id = cart.Id;
                });

            m_shopping_carts_mock.Setup(a => a.FindById(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return m_shopping_cartDb.SingleOrDefault(a => a.Id.Equals(id));

                });
        }

        [TestMethod]
        public void MockAddShoppingCartTest()
        {
            ShoppingCart addShoppingCart = new ShoppingCart()
            {
                Id = 0
            };

            m_shopping_carts_mock.Object.AddShoppingCart(addShoppingCart);
            Assert.AreEqual(4, m_shopping_carts_mock.Object.FindAll().Count());

        }

        [TestMethod]
        public void MockDeleteShoppingCartsTest()
        {
            ShoppingCart deleteShoppingCart = new ShoppingCart()
            {
                Id = 1
            };

            m_shopping_carts_mock.Object.DeleteShoppingCart(deleteShoppingCart);
            Assert.AreEqual(2, m_shopping_carts_mock.Object.FindAll().Count());

        }

        [TestMethod]
        public void MockUpdateShoppingCartTest()
        {
            ShoppingCart updateShoppingCart = new ShoppingCart()
            {
                Id = 1
            };

            m_shopping_carts_mock.Object.UpdateShoppingCart(updateShoppingCart);
            Assert.AreEqual(1, m_shopping_carts_mock.Object.FindById(1).Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FinalCST240;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ShoppingCartTests
{
    [TestClass]
    public class ItemMockTests
    {
        private Mock<IShoppingCart> m_item_mock;
        private IList<Item> m_itemDb;

        [TestInitialize()]
        public void Init()
        {
            m_itemDb = new List<Item>()
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

            m_item_mock = new Mock<IShoppingCart>();

            m_item_mock.Setup(a => a.FindAll()).Returns(
                () =>
                {
                    var items = new List<Item>();

                    m_itemDb.ToList().ForEach(item =>
                    {
                        items.Add(new Item()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice
                        });
                    });

                    return items;
                });

            m_item_mock.Setup(a => a.AddItem(It.IsAny<Item>())).Callback(
                (Item item) =>
                {
                    if (item == null) throw new InvalidOperationException("Add item - item was null");
                    if (item.Id <= 0)
                    {
                        item.Id = m_itemDb.Max(a => a.Id) + 1;
                        m_itemDb.Add(item);
                    }

                });

            m_item_mock.Setup(a => a.DeleteItem(It.IsAny<Item>())).Callback(
                (Item item) =>
                {
                    var deleteAccount = m_itemDb.SingleOrDefault(a => a.Id.Equals(item.Id));
                    m_itemDb.Remove(deleteAccount);
                });

            m_item_mock.Setup(a => a.UpdateItem(It.IsAny<Item>())).Callback(
                (Item item) =>
                {
                    if (item == null) throw new InvalidOperationException("Edit Item - item was null");
                    var updateItem = m_itemDb.SingleOrDefault(a => a.Id.Equals(item.Id));
                    if (updateItem == null) throw new InvalidOperationException("Item was null");
                    updateItem.Id = item.Id;
                    updateItem.Name = item.Name;
                    updateItem.Quantity = item.Quantity;
                    updateItem.UnitPrice = item.UnitPrice;
                });

            m_item_mock.Setup(a => a.FindById(It.IsAny<int>())).Returns(
                (int id) =>
                {
                    return m_itemDb.SingleOrDefault(a => a.Id.Equals(id));

                });
        }

        [TestMethod]
        public void MockAddItemTest()
        {
            Item newItem = new Item
            {
                Id = 0,
                Name = "canned beans",
                Quantity = 3,
                UnitPrice = 1.50m
            };

            m_item_mock.Object.AddItem(newItem);

            Assert.AreEqual(5, m_item_mock.Object.FindAll().Count());
        }

        [TestMethod()]
        public void MockDeleteItemTest()
        {
            Item deleteItem = new Item
            {
                Id = 4,
                Name = "grape bundle",
                Quantity = 3,
                UnitPrice = 0.99m,
            };
            m_item_mock.Object.DeleteItem(deleteItem);

            Assert.AreEqual(3, m_item_mock.Object.FindAll().Count());
        }

        [TestMethod()]
        public void MockUpdateItemTest()
        {
            Item updateItem = new Item
            {
                Id = 4,
                Name = "grape bundle",
                Quantity = 2,
                UnitPrice = 0.99m,
            };

            m_item_mock.Object.UpdateItem(updateItem);

            Assert.AreEqual(2, m_item_mock.Object.FindById(4).Quantity);
        }
    }
}

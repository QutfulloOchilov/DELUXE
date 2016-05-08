using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dawn.Model;
using System.Linq;
using Dawn.Model.Entities;

namespace TestModel
{
    [TestClass]
    public class OrderTest
    {
        User admin;
        Buying order;

        [TestMethod]
        public void Order()
        {

        }

        [TestMethod]
        public void Order_NewOrderTest()
        {

            Client newClient = new Client();
            newClient.FirstName = "Nozim";
            newClient.LastName = "Sharifov";
            order.Client = newClient;
            order.Date = DateTime.Now;

            var firstOrderDetail = new BuyingDetail
            {
                Count = 49,
                Buying = order,
                BuyingId = order.Guid
            };

            var firstProduct = new Product { Name = "Black paper", Price = 20, Count = 2000 };

            var firstFormula = new Formula { From = 1, To = 50, Product = firstProduct, Price = decimal.Parse("0.2") };
            firstProduct.Formulas.Add(firstFormula);

            var secondProduct = new Product { Name = "Color fuel", Price = 1, Count = 1000 };
            secondProduct.Formulas.Add(firstFormula);

            firstOrderDetail.Product = firstProduct;
            firstOrderDetail.ProductId = firstProduct.Guid;

            var secondOrderDetail = new BuyingDetail
            {
                Count = 20,
                Buying = order,
                BuyingId = order.Guid,
                Product = secondProduct
            };

            order.OrderDetails.Add(firstOrderDetail);
            order.OrderDetails.Add(secondOrderDetail);

            Assert.IsNotNull(order.OrderDetails);
            Assert.AreEqual(2, order.OrderDetails.Count, "Count of OrderDetauils should be 2");
            Assert.AreEqual(decimal.Parse("0.2"), firstOrderDetail.GetPrice());
            Assert.AreEqual(decimal.Parse("13.8"), order.OrderDetails.Select(od => od.CalculetOrderDetail()).Sum());
            Assert.IsNotNull(order.User);



        }

        [TestMethod]
        public void Order_UserTest()
        {

        }

        [TestInitialize]
        public void TestInit()
        {
            admin = new User();
            order = new Buying(admin);
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }

        #region Helper method

        private void ResetFilds()
        {
            admin = new User();
            admin.FirstName = "Qutfullo";
            admin.LastName = "Ochilov";
            order = new Buying(admin);
        }

        #endregion
    }
}

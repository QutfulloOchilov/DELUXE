using Deluxe.Framework;
using Deluxe.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    [TestClass]
    public class EntityBaseTest
    {

        [TestMethod]
        public void EntityBase_IdTest()
        {
            Assert.IsNotNull(new TestEntityBase().Guid, "If create new TestEntityBase Id should be not null or empty id");
        }

        public class TestEntityBase : EntityBase
        {

        }
    }
}

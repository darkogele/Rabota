using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.ExternalCC.HandlersHelper.Contracts;
using Interop.ExternalCC.HandlersHelper.HelperMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class CacheHelperTest
    {
        private CacheHelper cacheHelper;
        private int _count;

        [TestInitialize]
        public void SetUp()
        {
            cacheHelper = new CacheHelper();
            cacheHelper.Add("object1", "key1");
            cacheHelper.Add("object2", "key2");
            cacheHelper.Add("object3", "key3");
            cacheHelper.Add("object4", "key4");
            _count = cacheHelper.GetAllCount();
        }

        [TestCleanup]
        public void CleanUp()
        {
            cacheHelper = new CacheHelper();
            _count = 0;
        }

        [TestMethod]
        public void Add_Successfully()
        {
            cacheHelper.Add("testObject", "testKey");
            var count = cacheHelper.GetAllCount();
            Assert.AreEqual(_count + 1, count);
        }

        [TestMethod]
        public void Clear_Successfully()
        {
            cacheHelper.Clear("key2");
            var count = cacheHelper.GetAllCount();
            Assert.AreEqual(_count - 1, count);
        }

        [TestMethod]
        public void ClearAll_Successfully()
        {
            cacheHelper.ClearAll();
            var count = cacheHelper.GetAllCount();
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Exists_Successfully()
        {
            var exists = cacheHelper.Exists("key2");
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void Get_Successfully()
        {
            var cacheObject = cacheHelper.Get<string>("key2");
            Assert.AreEqual("object2", cacheObject);
        }

        [TestMethod]
        public void GetAllCount_Successfully()
        {
            var count = cacheHelper.GetAllCount();
            Assert.AreEqual(_count, count);
        }
    }
}

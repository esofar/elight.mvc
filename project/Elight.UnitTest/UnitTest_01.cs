using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Elight.Infrastructure;

namespace Elight.UnitTest
{
    [TestFixture]
    public class UnitTest_01
    {
        [Test]
        public void Test_JsonIgnore()
        {
            List<Product> ProductList = new List<Product>();
            ProductList.Add(new Product() { ShopID = 1, Price = 10, Count = 4, Name = "商品一" });
            ProductList.Add(new Product() { ShopID = 2, Price = 11, Count = 5, Name = "商品二" });
            ProductList.Add(new Product() { ShopID = 3, Price = 12, Count = 6, Name = "商品三" });
            string res = ProductList.ToJson("ShopID", "Name");
        }


    }

    public class Product
    {
        public int ShopID { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
    }
}

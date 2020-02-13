using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList
{
    /// <summary>
    /// Set class represent sets of products of Product type
    /// </summary>
    /// <param name="ProductList">Arra of Products in sets</param>
    class Set : Goods
    {
        Product[] ProductList;
        public Set(string name,decimal sum, Product[] products)
            :base(name,sum)
        {
            ProductList = products;
        }
        /// <summary>
        /// Display method return string with 
        /// information about product - its level-aggregated down hiearachy of classes
        /// </summary>
        /// <returns>Return formated string</returns>
        public override string Display()
        {
            string Str = base.Display() + $"\nGoods type: Set";
            return Str;
        }
        /// <summary>
        /// IsExpired check if the Expiration date is 
        /// has come 
        /// </summary>
        /// <returns>Return TRUE if the product is expired, otherwise false</returns>
        public override bool IsExpired()
        {
            foreach (Product item in ProductList)
            {
                if (item.IsExpired()) return true;
            }
            return false;
        }
    }
}

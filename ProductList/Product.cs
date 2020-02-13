using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProductList
{
    /// <summary>
    /// Product is child class of Goods -
    /// represent a product which is located somewhere on the storage
    /// </summary>
    /// <param name="ProductDate">Date of product manufacture</param>
    /// <param name="ExpirDate">Number of days from production date until the expiration</param>
    
    [Serializable]
    public class Product:  Goods
    {
       public DateTime ProductDate;
       public DateTime ExpirDate;
 
        public Product()
            :base("",0)
        {

        }
        public Product(DateTime productdate,int expirdays, string name, decimal sum)
            :base(name,sum)
        {
            ProductDate = productdate;
            ExpirDate = ProductDate.AddDays(expirdays);
        }

        
        /// <summary>
        /// Display method return string with 
        /// information about product - its level-aggregated down hiearachy of classes
        /// </summary>
        /// <returns>Return formated string</returns>
        public override string Display()
        {
            string Str=base.Display()+$"\nGoods type: product \nProduction date: {ProductDate.ToShortDateString()}\nExpiration date: {ExpirDate.ToShortDateString()}";
            return Str;
        }
        /// <summary>
        /// IsExpired check if the Expiration date is 
        /// has come 
        /// </summary>
        /// <returns>Return TRUE if the product is expired, otherwise false</returns>
        public override bool IsExpired()
        {
            return ExpirDate >= DateTime.Now ? false : true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList
{
    /// <summary>
    /// Consignment class represent consigment of production
    /// </summary>
    /// <param name="Amount">Number of products in consignment</param>
    /// <param name="ProductDate">Date of product manufacture</param>
    /// <param name="ExpirDate">Number of days from production date until the expiration</param>
    class Consignment : Goods
    {
        DateTime ProductDate;
        DateTime ExpirDate;
        uint Amount;
        public Consignment(DateTime productdate, int expirdays, string name, decimal sum,uint amount)
            : base(name, sum)
        {
            ProductDate = productdate;
            ExpirDate = ProductDate.AddDays(expirdays);
            Amount = amount;
        }
        /// <summary>
        /// Display method return string with 
        /// information about product - its level-aggregated down hiearachy of classes
        /// </summary>
        /// <returns>Return formated string</returns>
        public override string Display()
        {
            string Str = base.Display() + $"\nGoods type: consignment \nProduction date: {ProductDate.ToShortDateString()}\nExpiration date: {ExpirDate.ToShortDateString()}\nAmount: {Amount}";
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

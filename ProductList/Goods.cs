using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList
{
    /// <summary>
    /// Goods class is abstract class - use as base class
    /// for Product and other classes
    /// </summary>
    /// <param name="Name">Name of this object</param>
    /// <param name="Sum">Price of current object</param>
    public abstract  class Goods
    {
        public string Name { get; set; }
        public decimal Sum { get; set; }

        public Goods(string name, decimal sum)
        {
            Name = name;
            Sum = sum;
        }
        /// <summary>
        /// Display method return string with 
        /// information about product - its level-aggregated down hiearachy of classes
        /// </summary>
        /// <returns>Return formated string</returns>
        public virtual string Display()
        {
            return $"Name: {Name} \nSum: {Sum}";
        }
        /// <summary>
        /// Abstract class for future realisaions
        /// </summary>
        /// <returns></returns>
        public abstract bool IsExpired(); 
    }
}

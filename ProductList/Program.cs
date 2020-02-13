using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace ProductList
{
    class Program
    {
       /// <summary>
       /// Main function is program entry point
       /// </summary>
       /// <param name="ProductList">Array of product, input from Oracle Database</param>
       /// <param name="ConString">Connection string, used to connect to Oralce db</param>
        static void Main(string[] args)
        {
            //Product Milk = new Product(new DateTime(2021,1,30), 100, "Korovka", 76);
            //Console.WriteLine(Milk.Display());
            //Console.WriteLine(Milk.IsExpired());


            //Consignment Diary = new Consignment(DateTime.Now,15,"Molochnik postavka",15000,200);
            //Console.WriteLine(Diary.Display());
            //Console.WriteLine(Diary.IsExpired());
            int n=10;
            Product[] ProductList=new Product[n];

            string ConString = "DATA SOURCE=oraclebi.avalon.ru:1521/orcl12;PERSIST SECURITY INFO=True;USER ID=; PASSWORD=";
            ReadData(ConString,n, ref ProductList);

            Console.WriteLine(DateTime.Now.ToShortDateString());
            foreach (Product item in ProductList)
            {
                if (item != null) Console.WriteLine(item.Display()+"\nIs Expired: "+item.IsExpired()+"\n");
            }
            Console.ReadKey();
        }
        /// <summary>
        /// ReadData function establish connection with Oracle database 
        /// and retrieves rows from C#_PRODUCTS table to gain info about products
        /// </summary>
        /// <param name="connectionString">Connection string, used to connect to Oralce db</param>
        /// <param name="n">Number of rows to retrieve</param>
        /// <param name="prlist">Reference of array of Products</param>
        public static void ReadData(string connectionString, int n, ref Product[] prlist)
        {
            string queryString = "SELECT * FROM C#_PRODUCTS";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                connection.Open();
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    
                    int cnt= 0;
                    
                    Console.WriteLine("PRODUCT_ID |    PRODUCT_NAME     |   PRODUCTION_DATE    | EXPIRE_DATE   | PRICE ");
                    Console.WriteLine("________________________________________________________________________________");
                    while (reader.Read() && cnt<n &&(!reader.IsClosed))
                    {
                        prlist[cnt] = new Product(reader.GetDateTime(2), reader.GetInt32(3), reader.GetString(1), reader.GetDecimal(4));
                        prlist[cnt].Display();
                            Console.Write(reader.GetString(0).PadRight(15,' ' ));
                            Console.Write(reader.GetString(1).PadRight(22,' '));
                            Console.Write(reader.GetDateTime(2).ToShortDateString()+"                    ");
                            Console.Write(reader.GetOracleValue(3)+"        ");
                            Console.Write(reader.GetOracleValue(4) + "       ");
                        Console.WriteLine();
                        cnt++;
                    }
                }
            }
        }

    }
}



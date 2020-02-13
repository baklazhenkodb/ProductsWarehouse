using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
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
            int n=10;
            Product[] ProductList=new Product[n];
            XmlSerializer serializer = new XmlSerializer(typeof(Product));
            TextWriter writer = new StreamWriter("XMLOutput.xml");

            string ConString = "DATA SOURCE=oraclebi.avalon.ru:1521/orcl12;PERSIST SECURITY INFO=True;USER ID=; PASSWORD=";
            ReadData(ConString,n, ref ProductList);

            Console.WriteLine("\nCurrent date: "+DateTime.Now.ToShortDateString()+"\n");

            foreach (Product item in ProductList)
            {
                if (item != null) Console.WriteLine(item.Display()+"\nIs Expired: "+item.IsExpired()+"\n");
                serializer.Serialize(writer, item);
            }
            writer.Close();

            TextWriterTraceListener tr1 = new TextWriterTraceListener(System.Console.Out);
            Debug.Listeners.Add(tr1);
            TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText("Output.txt"));
            Debug.Listeners.Add(tr2);
            Trace.WriteLine("Trace Information-Product Starting ");
            Trace.Indent();
           
            Trace.WriteLineIf(n > 0, "The number of rows is correct");
            Trace.Assert(ProductList[n - 1] == null, "Number of rows was bigger when num of rows exist in database");

            Trace.Unindent();
            Trace.WriteLine("Trace Information-Product Ending");

            Trace.Flush();
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



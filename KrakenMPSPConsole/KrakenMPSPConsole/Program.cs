using System;
using System.Linq;
using System.Net;

namespace KrakenMPSPConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Percorrendo dados
                // Deve chamar via HTTP

                using (var http = new WebClient())
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Search execution error: {0}", e.Message);
            }
            Console.WriteLine("finished application");
            Console.ReadKey();
        }

        static void CopyValues<T>(T target, object source)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var targetType = prop.PropertyType.ToString();
                var sourceType = source.GetType().ToString();
                if (targetType == sourceType)
                {
                    prop.SetValue(target, source);
                }
            }
        }
    }
}

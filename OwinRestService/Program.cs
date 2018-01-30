using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace OwinRestService
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://127.0.0.1:8080/";

            // Start OWIN host 
            using (WebApp.Start(url: baseAddress))
            {
                Console.WriteLine("Service Listening at " + baseAddress);

                System.Threading.Thread.Sleep(-1);
            }
        }
    }
}
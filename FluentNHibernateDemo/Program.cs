using System;

namespace FluentNHibernateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = new Customer
                    {
                        FirstName = "Pierre",
                        LastName = "Perinet"
                    };

                    session.Save(customer);
                    transaction.Commit();
                    Console.WriteLine("Customer Created: " + customer.FirstName + "\t" + customer.LastName);
                }
                Console.ReadKey();
            }
        }
    }
}
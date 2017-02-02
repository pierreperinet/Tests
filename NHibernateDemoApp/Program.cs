using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

using System;
using System.Linq;
using System.Reflection;

namespace NHibernateDemoApp
{
    // https://www.tutorialspoint.com/nhibernate/index.htm

    class Program
    {

        static void Main(string[] args)
        {
            var cfg = new Configuration();

            cfg.DataBaseIntegration(x => { x.ConnectionString = "Data Source=PERINET4;Initial Catalog=NHibernateDemoDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                x.LogSqlInConsole = true;
            });

            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            var sefact = cfg.BuildSessionFactory();

            using (var session = sefact.OpenSession())
            {

                using (var tx = session.BeginTransaction())
                {

                    var student1 = new Student
                    {
                        ID = 1,
                        FirstName = "Adrien",
                        LastName = "Perinet",
                        AcademicStanding = StudentAcademicStanding.Excellent
                    };

                    var student2 = new Student
                    {
                        ID = 2,
                        FirstName = "Pierre",
                        LastName = "Perinet",
                        AcademicStanding = StudentAcademicStanding.Good
                    };

                    session.Save(student1);
                    session.Save(student2);
                    tx.Commit();


                    var students = session.CreateCriteria<Student>().List<Student>();

                    foreach (var s in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2} \t{3}", s.ID, s.FirstName, s.LastName, s.AcademicStanding);
                    }
                    var student = session.Get<Student>(1);
                    Console.WriteLine("Retrieved by ID");
                    Console.WriteLine("{0} \t{1} \t{2} \t{3}", student.ID, student.FirstName, student.LastName, student.AcademicStanding);

                    Console.WriteLine("Update the first name of ID = {0}", student.ID);
                    student.FirstName = "Lara";
                    student.LastName = "Perinet";
                    student.AcademicStanding = StudentAcademicStanding.Excellent;
                    session.Update(student);
                    foreach (var s in students)
                    {
                        Console.WriteLine("{0} \t{1} \t{2} \t{3}", s.ID, s.FirstName, s.LastName, s.AcademicStanding);
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
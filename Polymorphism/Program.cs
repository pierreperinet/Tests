using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            // Polymorphism
            IAnimal animal = new Dog();
            IAnimal animalTwo = new Cat();
            Console.WriteLine(animal.Name);
            Console.WriteLine(animalTwo.Name);

            // Inheritence
            var subClass = new SubClass();
            Console.WriteLine(subClass.HelloMessage);
            Console.WriteLine(subClass.ArbitraryMessage);

            Console.ReadKey();
        }
    }
}

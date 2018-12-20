using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structural.Composite
{
    public class CompositeTest : IRunable
    {
        public void Run()
        {
            List<Child> children = new List<Child>();

            var parentLvl1 = new Parent("Joro", 1);
            parentLvl1.Children.Add(new Child("Jorko Jr"));
            parentLvl1.Children.Add(new Child("Jenevir"));

            var p1 = new Parent("Dimitrinka", 2);
            p1.Children.Add(new Child("Clementine"));

            var parentLvl2 = new Parent("Goshka", 1);
            parentLvl2.Children.Add(p1);
            parentLvl2.Children.Add(new Child("Kirk"));

            children.Add(new Child("Bob"));
            children.Add(parentLvl1);
            children.Add(parentLvl2);
            children.Add(new Child("Stratsimir"));

            foreach (var child in children)
            {
                Console.WriteLine(child);
            }
        }
    }
}

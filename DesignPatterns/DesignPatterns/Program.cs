using DesignPatterns.Structural.Adapter;
using DesignPatterns.Structural.Bridge;
using DesignPatterns.Structural.Composite;
using System;

namespace DesignPatterns
{
    class Program
    {
        static void Main()
        {
            var adapterTest = new CompositeTest();
            adapterTest.Run();
        }
    }
}

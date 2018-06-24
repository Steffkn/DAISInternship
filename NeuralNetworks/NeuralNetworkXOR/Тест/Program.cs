using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fun
{
    class Program
    {
        static void Main(string[] args)
        {
            var aaa = Regex.Matches("1 2 3 4 5 6", @"[\d]+").Cast<Match>().Select(a => a.Value).ToArray();
            var aa = Regex.Matches("1 2 3 4 5 6", @"[\d]+").Cast<Match>();

            Console.WriteLine(String.Join(", ", aaa));
            Console.WriteLine(String.Join(", ", aa));
            var objs = new List<SomeFunObject>();
            objs.Add(new SomeFunObject("edno"));
            objs.Add(new SomeFunObject("dve"));
            objs.Add(new SomeFunObject("tri"));

            Console.WriteLine(String.Join(", ", objs));

            Console.WriteLine(String.Join(", ", objs.Select(a=>a.Value).ToArray()));
        }
    }

    public class SomeFunObject
    {
        public string Value { get; set; }
        public SomeFunObject(string value)
        {
            this.Value = value;
        }
    }
}

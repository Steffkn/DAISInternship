using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Structural.Composite
{
    public class Parent : Child
    {
        public Parent(string name, int level)
            : base(name)
        {
            this.Level = level;
            this.Children = new List<Child>();
        }

        public List<Child> Children { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(base.ToString());

            foreach (var child in this.Children)
            {
                stringBuilder.Append(new string(' ', Level * 2));
                stringBuilder.AppendLine(child.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}

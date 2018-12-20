
namespace DesignPatterns.Structural.Composite
{
    public class Child
    {
        public Child(string name)
        {
            this.Name = name;
            this.Level = 1;
        }

        public int Level { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}

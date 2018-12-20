namespace DesignPatterns.Structural.Bridge
{
    public abstract class Counter
    {
        public int Count { get; set; }

        public abstract void Tick();

        public override string ToString()
        {
            return string.Format("{0}'s current count: {1}", GetType().Name, this.Count);
        }
    }
}

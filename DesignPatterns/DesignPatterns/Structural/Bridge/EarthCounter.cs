namespace DesignPatterns.Structural.Bridge
{
    public class EarthCounter : Counter
    {
        public override void Tick()
        {
            // bridge implementation
            this.Count += 1;
        }
    }
}

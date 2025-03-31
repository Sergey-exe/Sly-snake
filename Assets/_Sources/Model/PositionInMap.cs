namespace _Sources.Model
{
    public struct PositionInMap
    {
        public int X { get; }
            
        public int Y { get; }

        public PositionInMap(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

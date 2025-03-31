namespace _Sources.Model
{
    public class ButtonInMap
    {
        public PositionInMap Position { get; private set; }
        
        public int Order { get; private set; }

        public ButtonInMap(PositionInMap position, int order)
        {
            Position = position;
            Order = order;
        }
    }
}
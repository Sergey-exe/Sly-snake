using _Sources.Model;
using UnityEngine;

namespace _Sources.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MapElement : MonoBehaviour
    {
        private int NewIndex;
    
        [field: SerializeField] public int Index { get; private set; }
    
        public PositionInMap Position { get; private set; }

        public void Init(int index, PositionInMap positionInPositionInMap)
        {
            Position = positionInPositionInMap;
            SetIndex(index);
        }

        public void SetIndex(int index)
        {
            NewIndex = index;
        }

        public void ChangeIndex()
        {
            Index = NewIndex;
        }
    }
}

using _Sources.Model;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MapElement : MonoBehaviour
{
    private int NewIndex;
    
    [field: SerializeField] public int Index { get; private set; }
    
    public MapVector2 Position { get; private set; }

    public void Init(int index, MapVector2 positionInMap)
    {
        Position = positionInMap;
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

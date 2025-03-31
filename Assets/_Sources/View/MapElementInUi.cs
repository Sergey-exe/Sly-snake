using _Sources.Model;
using UnityEngine;

public class MapElementInUi : MonoBehaviour
{
    public PositionInMap Position { get; private set; }

    public void Init(PositionInMap position)
    {
        Position = position;
    }
}

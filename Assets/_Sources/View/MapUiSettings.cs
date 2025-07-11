using UnityEngine;

public class MapUiSettings : MonoBehaviour
{
    [field: SerializeField] public float Step { get; private set; }

    [field: SerializeField] public float Offset { get; private set; }


    [field: SerializeField] public MapElementInUi EmptyElementPrefabInUi { get; private set; }
    
    [field: SerializeField] public GameObject FailGameWindow { get; private set; }
    
    [field: SerializeField] public GameObject WinGameWindow { get; private set; }
}

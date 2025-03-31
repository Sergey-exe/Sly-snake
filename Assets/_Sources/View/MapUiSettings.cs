using UnityEngine;

public class MapUiSettings : MonoBehaviour
{
    [field: SerializeField] public MapElementInUi EmptyElementPrefabInUi { get; private set; }
    
    [field: SerializeField] public GameObject FailGameWindow { get; private set; }
    
    [field: SerializeField] public GameObject WinGameWindow { get; private set; }
}

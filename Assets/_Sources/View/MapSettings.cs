using Array2DEditor;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class MapSettings : MonoBehaviour
{ 
    [ SerializedDictionary("Код элемента", "Спрайт элемента")] 
    public SerializedDictionary<int, Sprite> MapElementsPrefabs;
    
    [field: SerializeField] public Array2DInt Map { get; private set; }

    [field: SerializeField] public MapElement EmptyElementPrefab { get; private set; }
}

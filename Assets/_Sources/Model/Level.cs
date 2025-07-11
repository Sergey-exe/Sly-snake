using Array2DEditor;
using UnityEngine;

namespace _Sources.Model
{
    [CreateAssetMenu(fileName = "New level", menuName = "Levels/Crete new level", order = 51)]
    public class Level : ScriptableObject
    {
        [field: SerializeField] public string LevelName {get; private set;}
        [field: Space(20)]
        [field: SerializeField] public Array2DInt Map { get; private set; }

        public int[,] GetCells()
        {
            return Map.GetCells();
        }
    }
}

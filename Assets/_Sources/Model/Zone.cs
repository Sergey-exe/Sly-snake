using System;
using System.Collections.Generic;
using _Sources.View;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Sources.Model
{
    [CreateAssetMenu(fileName = "New zone", menuName = "Levels/Crete new zone", order = 51)]
    public class Zone : ScriptableObject
    
    {
        [field: SerializeField] public string ZoneName { get; private set; }
        
        [field: SerializeField] public Color BackgroundsColor { get; private set; }
        
        [field: SerializeField] public AudioClip ZoneMusic { get; private set; }
    
        [Space(20)]
        [Tooltip("> 0 - твёрдые плитки, < 0 плитки, на которые может попасть игрок :)")]
        [SerializedDictionary("Код элемента", "Спрайт элемента")] 
        public SerializedDictionary<int, Sprite> MapElementsSprites;
        [Space(20)]
        [SerializeField] private List<Level> _levels;

        public int[,] GetMap(int levelIndex)
        {
            if (levelIndex < 0)
                throw new IndexOutOfRangeException(nameof(levelIndex));
            
            if (levelIndex >= _levels.Count)
                throw new IndexOutOfRangeException(nameof(levelIndex));
            
            return _levels[levelIndex].GetCells();
        }

        public Color GetBackgroundColor()
        {
            return BackgroundsColor;
        }

        public bool HasLevel(int levelIndex)
        {
            if (levelIndex < 0)
                return false;
            
            if (levelIndex >= _levels.Count)
                return false;
            
            return true;
        }
    } 
}

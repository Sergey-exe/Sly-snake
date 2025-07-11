using System;
using System.Collections.Generic;
using _Sources.Model;
using UnityEngine;

namespace _Sources.View
{
    public class MapSettings : MonoBehaviour
    {
        [SerializeField] private Zone[] _zones;
        
        private Zone _currentZone; 
        private int _currentLevelIndex;
        private int _currentZoneIndex;
        private bool _haveLevelIndexes;
        
        [field: SerializeField] public MapElement EmptyElementPrefab { get; private set; }

        public void SetLevelIndexes(int currentLevelIndex, int currentZoneIndex)
        {
            if(currentLevelIndex < 0 )
                throw new ArgumentOutOfRangeException(nameof(currentLevelIndex));
            
            if(currentZoneIndex < 0 )
                throw new ArgumentOutOfRangeException(nameof(currentZoneIndex));
            
            _currentLevelIndex = currentLevelIndex;
            _currentZoneIndex = currentZoneIndex;
            _currentZone = _zones[_currentZoneIndex];
            
            _haveLevelIndexes = true;
        }

        public Dictionary<int, Sprite> GetMapElementsSprites()
        {
            if(_haveLevelIndexes == false)
                throw new Exception("class is not active");
            
            return _currentZone.MapElementsSprites;
        }

        public int[,] GetMap()
        {
            if(_haveLevelIndexes == false)
                throw new Exception("class is not active");
            
            return _currentZone.GetMap(_currentLevelIndex);
        }

        public Color GetBackgroundColor()
        {
            if(_haveLevelIndexes == false)
                throw new Exception("class is not active");
            
            return _currentZone.GetBackgroundColor();
        }

        public bool TrySetNextLevel()
        {
            if(_haveLevelIndexes == false)
                throw new Exception("class is not active");
            
            if (_currentZone.HasLevel(_currentLevelIndex + 1) == false)
            {
                if(_currentZoneIndex + 1 >= _zones.Length)
                    return false;
                
                _currentZoneIndex++;
                _currentZone = _zones[_currentZoneIndex];
                _currentLevelIndex = 0;
                
                return true;
            }
            
            _currentLevelIndex++;
            return true;
        }
    }
}

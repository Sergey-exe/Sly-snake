using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Sources.Model
{
    public class MapData
    {
        private MapVector2 _playerPosition;
        
        private enum _mapElements
        {
            SpaceToFill = 0,
            Wall = 1,
            Player = 2,
            TailPlayer = 3,
        };
        
        private int[,] _map;
        
        public int GetPlayerIndex => (int)_mapElements.Player;
        
        public MapData(int[,] map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
            ShowPlayer();
        }

        public List<MapVector2> TrySetNewPlayerPosition(MapVector2 derection)
        {
            List<MapVector2> сhangeableElementsPositions = new List<MapVector2>();
            
            int newX = _playerPosition.X;
            int newY = _playerPosition.Y;
            
            for (int i = _playerPosition.X; i < _map.GetLength(0); i += derection.X)
            {
                if (TryChangePosition(i, _playerPosition.Y, ref newX, ref newY))
                    break;
                
                сhangeableElementsPositions.Add(new MapVector2(newX, newY));
            }
            
            for (int i = _playerPosition.Y; i < _map.GetLength(1); i += derection.Y)
            {
                if (TryChangePosition(_playerPosition.X, i, ref newX, ref newY))
                    break;
                
                сhangeableElementsPositions.Add(new MapVector2(newX, newY));
            }
            
            return сhangeableElementsPositions;
        }

        private bool TryChangePosition(int currentX, int currentY, ref int newX, ref int newY)
        {
            if (_map[currentX, currentY] == (int)_mapElements.Wall || _map[currentX, currentY] == (int)_mapElements.TailPlayer)
            {
                _playerPosition = new MapVector2(newX, newY);
                _map[newX, newY] = (int)_mapElements.Player;

                return true;
            }

            _map[currentX, currentY] = (int)_mapElements.TailPlayer;
            newX = currentX;
            newY = currentY;
            
            return false;
        }
        
        public int[,] GetMap()
        {
            return _map;
        }

        private void ShowPlayer()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j <  _map.GetLength(1); j++)
                {
                    if (_map[i, j] == (int)_mapElements.Player)
                    {
                        _playerPosition = new MapVector2(i, j);
                        
                        return;
                    }
                }
            }
        }
    }
}
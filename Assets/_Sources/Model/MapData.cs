using System;
using System.Collections.Generic;

namespace _Sources.Model
{
    public class MapData
    {
        private PositionInMap _playerPosition;
        private ButtonsData _buttonsData;
        private TrapData _trapData;
        
        private int[,] _startMap;
        private int[,] _map;

        private bool _hasMap;

        private int[] _ignoreIndexes = 
        {
            (int)_mapElements.Trap,
        };
        
        private enum _mapElements
        {
            Trap = -4,
            ThreeButton = -3,
            TwoButton = -2,
            OneButton = -1,
            SpaceToFill = 0,
            Wall = 1,
            Player = 2,
            TailPlayer = 3,
        };
        
        public MapData()
        {
            _trapData = new TrapData();
            _buttonsData = new ButtonsData();
        }
        
        public int GetPlayerIndex => (int)_mapElements.Player;

        public void SetMap(int[,] map)
        {
            _startMap = (int[,])map.Clone();
            _map = (int[,])map.Clone();
            
            ShowPlayer();
            AddButtons();
            AddTraps();
            
            _hasMap = true;
        }

        public List<PositionInMap> TrySetNewPlayerPosition(PositionInMap derection, out bool? hasFreeSpace, out bool areVictoryConditionsMet)
        {
            List<PositionInMap> сhangeableElementsPositions = new List<PositionInMap>();
            
            int newX = _playerPosition.X;
            int newY = _playerPosition.Y;
            
            for (int i = _playerPosition.X; i < _map.GetLength(0); i += derection.X)
            {
                if (TryChangePosition(i, _playerPosition.Y, ref newX, ref newY))
                    break;
                
                сhangeableElementsPositions.Add(new PositionInMap(newX, newY));
            }
            
            for (int i = _playerPosition.Y; i < _map.GetLength(1); i += derection.Y)
            {
                if (TryChangePosition(_playerPosition.X, i, ref newX, ref newY))
                    break;
                
                сhangeableElementsPositions.Add(new PositionInMap(newX, newY));
            }

            hasFreeSpace = TryShowFreeSpace();
            areVictoryConditionsMet = AreVictoryConditionsMet();
            
            return сhangeableElementsPositions;
        }
        
        public int[,] GetMap()
        {
            return _map;
        }

        public void Revert()
        {
            _map = (int[,])_startMap.Clone();
            ShowPlayer();
            
            _buttonsData.Revert();
            _trapData.Revert();
        }

        private bool AreVictoryConditionsMet()
        {
            if (_buttonsData.AreButtonsPressedInOrder() == false)
                return false;
            
            if(_trapData.IsPlayerInTrap)
                return false;
                
            return true;
        }

        private bool? TryShowFreeSpace()
        {
            int step = 1;
            
            PositionInMap[] aroundPoints =
            {
                new (_playerPosition.X - step, _playerPosition.Y ),
                new (_playerPosition.X + step, _playerPosition.Y ),
                new (_playerPosition.X, _playerPosition.Y - step),
                new (_playerPosition.X, _playerPosition.Y + step),
            };

            foreach (var point in aroundPoints)
            {
                if (_map[point.X, point.Y] <= (int)_mapElements.SpaceToFill & TryShowInIgnore(_map[point.X, point.Y]) == false)
                {
                    return null;
                }
            }

            foreach (var mapElement in _map)
            {
                if (mapElement <= (int)_mapElements.SpaceToFill & TryShowInIgnore(mapElement) == false)
                    return false;
            }
            
            return true;
        }

        private bool TryChangePosition(int currentX, int currentY, ref int newX, ref int newY)
        {
            if (_map[currentX, currentY] == (int)_mapElements.Wall || _map[currentX, currentY] == (int)_mapElements.TailPlayer)
            {
                _playerPosition = new PositionInMap(newX, newY);
                _map[newX, newY] = (int)_mapElements.Player;

                return true;
            }
            
            ActivateFunctionalElements(currentX, currentY);
            _map[currentX, currentY] = (int)_mapElements.TailPlayer;
            newX = currentX;
            newY = currentY;
            
            return false;
        }

        private void ActivateFunctionalElements(int positionX, int positionY)
        {
            PositionInMap currentPosition = new PositionInMap(positionX, positionY);
            _trapData.TryShowTrap(currentPosition);
            _buttonsData.TryDownButton(currentPosition);
        }

        private void ShowPlayer()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j <  _map.GetLength(1); j++)
                {
                    if (_map[i, j] == (int)_mapElements.Player)
                    {
                        _playerPosition = new PositionInMap(i, j);
                        return;
                    }
                }
            }
        }

        private void AddButtons()
        {
            int signChangeMultiplier = -1;
            
            List<ButtonInMap> buttons = new List<ButtonInMap>();
            
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j <  _map.GetLength(1); j++)
                {
                    if (_map[i, j] == (int)_mapElements.OneButton || _map[i, j] == (int)_mapElements.TwoButton
                                                                  || _map[i, j] == (int)_mapElements.ThreeButton)
                    {
                        buttons.Add(new ButtonInMap(new PositionInMap(i, j), _map[i, j] * signChangeMultiplier));
                    }
                }
            }
            
            _buttonsData.AddButtons(buttons);
        }

        private void AddTraps()
        {
            List<PositionInMap> traps = new List<PositionInMap>();
            
            for (int i = 0; i < _map.GetLength(0); i++)
            {
                for (int j = 0; j <  _map.GetLength(1); j++)
                {
                    if (_map[i, j] == (int)_mapElements.Trap)
                        traps.Add(new PositionInMap(i, j));
                }
            }
            
            _trapData.AddTraps(traps);
        }

        private bool TryShowInIgnore(int currentIndex)
        {
            foreach (var index in _ignoreIndexes)
            {
                if(index == currentIndex)
                    return true;
            }
            
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using _Sources.Model;
using UnityEngine;

namespace _Sources.View
{
    public class MapPainter : MonoBehaviour
    {
        private MapElement _emptyElementPrefab;
        private PlayerMover _playerMover;
        private Transform _startPlayerTransform;
        private List<MapElement> _mapElements;
        private Dictionary<int, Sprite> _mapElementsSprites;
        private int _playerIndex;
        private bool _isInit;
        private bool _isActive;
    
        public bool MapChanging {get; private set;}
    
        private void OnDisable()
        {
            _playerMover.InMapElement -= ChangeMapElement;
            _playerMover.Finished -= MapChanged;
        }
        
        public void Activate()
        {
            if(_isInit == false)
                return;
            
            _isActive = true;
        }
        
        public void Init(MapElement emptyElementPrefab, PlayerMover playerMover, int playerIndex, Dictionary<int, Sprite> mapElementsSprites)
        {
            _emptyElementPrefab = emptyElementPrefab ?? throw new ArgumentNullException(nameof(emptyElementPrefab));
            _playerMover = playerMover ?? throw new ArgumentNullException(nameof(playerMover));
            _mapElementsSprites = mapElementsSprites ?? throw new ArgumentNullException(nameof(mapElementsSprites));
        
            _playerIndex = playerIndex;
        
            _playerMover.InMapElement += ChangeMapElement;
            _playerMover.Finished += MapChanged;
        
            _mapElements = new List<MapElement>();
            _isInit = true;
        }

        public void ChangeMap(List<PositionInMap> сhangeableElementsPositions, int[,] map)
        {
            if(_isActive == false)
                return;

            if (_mapElements == null)
                throw new ArgumentNullException("Карта должна быть создана для отображения");
        
            MapChanging = true;
        
            List<MapElement> changeableElements = new List<MapElement>();
        
            foreach (var position in сhangeableElementsPositions)
            {
                foreach (var element in _mapElements)
                {
                    if (element.Position.X == position.X & element.Position.Y == position.Y)
                    {
                        element.SetIndex(map[position.X, position.Y]);
                        changeableElements.Add(element);
                    }
                }
            }
        
            _playerMover.SetMovementPosition(changeableElements);
            _playerMover.StartMove();
        }

        public void CreateMap(int[,] map)
        {
            if(_isInit == false)
                return;
        
            float elementScaleX = 0;
            float elementScaleY = 0;
            float linePositionY = transform.position.y;
        
            var mapTransformPositon = transform.position;
        
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(_mapElementsSprites.ContainsKey(map[i, j]))
                    {
                        MapElement mapElement = Instantiate(_emptyElementPrefab, mapTransformPositon, Quaternion.identity);
                    
                        ChangeElement(mapElement, map[i, j]);

                        elementScaleX = mapElement.transform.localScale.x;
                        elementScaleY = mapElement.transform.localScale.y;
                    
                        mapElement.Init(map[i, j], new PositionInMap(i, j));
                        _mapElements.Add(mapElement);
                    
                        TrySetStartPlayerPositon(mapElement.Index, mapElement.transform);
                    
                        mapTransformPositon = new Vector2(mapTransformPositon.x + elementScaleX, mapTransformPositon.y);
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Неизвестный ключ элемента! Элемент {map[i, j]} не найден!");
                    }
                }
            
                linePositionY -= elementScaleY;
                mapTransformPositon = new Vector2(transform.position.x, linePositionY);
            }
        }
        
        public void RevertMap(int[,] map)
        {
            _playerMover.StopMove();
            MapChanging = false;
            
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    foreach (var element in _mapElements)
                    {
                        if (element.Position.X == i && element.Position.Y == j)
                        {
                            if (map[i, j] != element.Index)
                            {
                                ChangeElement(element, map[i, j]);
                                TrySetStartPlayerPositon(element.Index, element.transform);
                            }
                        }
                    }
                }
            }
        }
        
        [ContextMenu("Инструменты/Очистить карту")]
        public void ClearMap()
        {
            if(_isInit == false)
                return;
            
            for (int i = _mapElements.Count - 1; i >= 0; i--)
            {
                Destroy(_mapElements[i].gameObject);
            }
        }

        public Transform GetStartTransform()
        {
            if (_startPlayerTransform == null)
                throw new ArgumentNullException(nameof(_startPlayerTransform));
        
            return _startPlayerTransform;
        }
        
        private void ChangeElement(MapElement mapElement, int newIndex)
        {
            mapElement.SetIndex(newIndex);
            mapElement.ChangeIndex();
            
            Sprite elementSprite = _mapElementsSprites[mapElement.Index];
            mapElement.GetComponent<SpriteRenderer>().sprite = elementSprite;
            mapElement.name = elementSprite.name;
        }

        private void TrySetStartPlayerPositon(int index, Transform elementTransform)
        {
            if (index != _playerIndex)
                return; 
            
            _startPlayerTransform = elementTransform;
            
            if (_isActive == false)
                return;
            
            _playerMover.SetPosition(_startPlayerTransform);
        }

        private void ChangeMapElement(MapElement element)
        {
            element.ChangeIndex();
            element.GetComponent<SpriteRenderer>().sprite = _mapElementsSprites[element.Index];
        }

        private void MapChanged()
        {
            MapChanging = false;
        }
    }
}

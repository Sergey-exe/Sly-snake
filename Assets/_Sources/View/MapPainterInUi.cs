using System;
using System.Collections.Generic;
using _Sources.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _Sources.View
{
    public class MapPainterInUi : MonoBehaviour
    {
        private float _step;
        private float _offset;
        private MapElementInUi _emptyElementPrefab;
        private List<MapElementInUi> _mapElements;
        private Dictionary<int, Sprite> _mapElementsSprites;
        private RectTransform _rectTransform;
        private bool _hasSprites;
        private bool _isInit;
        private bool _isActive;
        
        public void Init(float step, float offset, MapElementInUi emptyElementPrefab)
        {
            _emptyElementPrefab = emptyElementPrefab ?? throw new ArgumentNullException(nameof(emptyElementPrefab));
        
            _mapElements = new List<MapElementInUi>();
            _rectTransform = GetComponent<RectTransform>();
            
            _step = step;
            _offset = offset;
            
            _isInit = true;
        }
        
        public void Activate()
        {
            if(_isInit == false)
                return;
            
            _isActive = true;
        }

        public void SetSprites(Dictionary<int, Sprite> mapElementsSprites)
        {
            _mapElementsSprites = mapElementsSprites ?? throw new ArgumentNullException(nameof(mapElementsSprites));
            
            _hasSprites = true;
        }
        
        public void ChangeMap(int[,] map)
        {
            if(_isActive == false)
                return;

            if (_mapElements == null)
                throw new ArgumentNullException("Карта должна быть создана для отображения");
        
            foreach (var element in _mapElements)
            {
                ChangeSprite(element, map[element.Position.X, element.Position.Y]);
            }
        }

        public void CreateMap(int[,] map)
        {
            if(_isActive == false)
                return;
            
            if (_hasSprites == false)
                throw new ArgumentNullException("Спрайты не установлены!");
            
            float linePositionY = _rectTransform.position.y;

            var mapTransformPositon = _rectTransform.position;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                linePositionY -= _step;
                mapTransformPositon = new Vector3(_rectTransform.position.x + _offset * i, linePositionY, mapTransformPositon.z);
                
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (_mapElementsSprites.ContainsKey(map[i, j]))
                    {
                        MapElementInUi mapElement = Instantiate(_emptyElementPrefab, mapTransformPositon, _rectTransform.rotation);
                        mapElement.transform.SetParent(transform);
                        
                        mapElement.Init(new PositionInMap(i, j));
                        _mapElements.Add(mapElement);

                        mapTransformPositon = new Vector3(mapTransformPositon.x + _step, mapTransformPositon.y + _offset, mapTransformPositon.z);
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Неизвестный ключ элемента! Элемент {map[i, j]} не найден!");
                    }
                }
            }
        }

        private void ChangeSprite(MapElementInUi mapElement, int index)
        {
            if (_hasSprites == false)
                throw new ArgumentNullException("Спрайты не установлены!");
            
            Sprite elementSprite = _mapElementsSprites[index];
            mapElement.GetComponent<Image>().sprite = elementSprite;
            mapElement.name = elementSprite.name;
        }
    }
}

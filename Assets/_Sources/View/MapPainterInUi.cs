using System;
using System.Collections.Generic;
using _Sources.Model;
using UnityEngine;
using UnityEngine.UI;

namespace _Sources.View
{
    public class MapPainterInUi : MonoBehaviour
    {
        private MapElementInUi _emptyElementPrefab;
        private List<MapElementInUi> _mapElements;
        private Dictionary<int, Sprite> _mapElementsSprites;
        private RectTransform _rectTransform;
        private bool _isInit;
        private bool _isActive;
        
        public void Init(MapElementInUi emptyElementPrefab, Dictionary<int, Sprite> mapElementsSprites)
        {
            _emptyElementPrefab = emptyElementPrefab ?? throw new ArgumentNullException(nameof(emptyElementPrefab));
            _mapElementsSprites = mapElementsSprites ?? throw new ArgumentNullException(nameof(mapElementsSprites));
        
            _mapElements = new List<MapElementInUi>();
            _rectTransform = GetComponent<RectTransform>();
            _isInit = true;
        }
        
        public void Activate()
        {
            if(_isInit == false)
                return;
            
            _isActive = true;
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
            
            float elementScaleX = 0;
            float elementScaleY = 0;
            float linePositionY = _rectTransform.position.y;

            var mapTransformPositon = _rectTransform.position;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (_mapElementsSprites.ContainsKey(map[i, j]))
                    {
                        MapElementInUi mapElement = Instantiate(_emptyElementPrefab, mapTransformPositon, Quaternion.identity);
                        RectTransform elementRectTransform = mapElement.GetComponent<RectTransform>();
                        mapElement.transform.SetParent(transform);

                        elementScaleX = elementRectTransform.sizeDelta.x;
                        elementScaleY = elementRectTransform.sizeDelta.y;
                        
                        mapElement.Init(new PositionInMap(i, j));
                        _mapElements.Add(mapElement);

                        mapTransformPositon = new Vector2(mapTransformPositon.x + elementScaleX, mapTransformPositon.y);
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Неизвестный ключ элемента! Элемент {map[i, j]} не найден!");
                    }
                }

                linePositionY -= elementScaleY;
                mapTransformPositon = new Vector2(_rectTransform.position.x, linePositionY);
            }
        }

        private void ChangeSprite(MapElementInUi mapElement, int index)
        {
            Sprite elementSprite = _mapElementsSprites[index];
            mapElement.GetComponent<Image>().sprite = elementSprite;
            mapElement.name = elementSprite.name;
        }
    }
}

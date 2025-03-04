using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.Model;
using UnityEngine;
using UnityEngine.UIElements;

public class MapPainter : MonoBehaviour
{
    private MapElement _emptyElementPrefab;
    private PlayerMoveAnimator _playerMoveAnimator;
    private Transform _playerTransform;
    private List<MapElement> _mapElements;
    private Dictionary<int, Sprite> _mapElementsSprites;
    private int _playerIndex;
    private bool _isInit;
    
    private void OnDisable()
    {
        _playerMoveAnimator.Finished -= ChangeMapElement;
    }

    public void Init(MapElement emptyElementPrefab, PlayerMoveAnimator playerMoveAnimator, int playerIndex, Dictionary<int, Sprite> mapElementsPrefabs)
    {
        _emptyElementPrefab = emptyElementPrefab ?? throw new ArgumentNullException(nameof(emptyElementPrefab));
        _playerMoveAnimator = playerMoveAnimator ?? throw new ArgumentNullException(nameof(playerMoveAnimator));
        _mapElementsSprites = mapElementsPrefabs ?? throw new ArgumentNullException(nameof(mapElementsPrefabs));
        
        _playerIndex = playerIndex;
        
        _playerMoveAnimator.Finished += ChangeMapElement;
        
        _mapElements = new List<MapElement>();
        _isInit = true;
    }

    public void ChangeMap(List<MapVector2> сhangeableElementsPositions, int[,] map)
    {
        if(_isInit == false)
            return;

        if (_mapElements == null)
            throw new ArgumentNullException("Карта должна быть создана для отображения");
        
        
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

        
        _playerMoveAnimator.SetMovementPosition(changeableElements);
        _playerMoveAnimator.StartMove();
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
                    mapElement.SetIndex(map[i, j]);
                    mapElement.ChangeIndex();
                    
                    Sprite elementSprite = _mapElementsSprites[mapElement.Index];
                    mapElement.GetComponent<SpriteRenderer>().sprite = elementSprite;
                    
                    elementScaleX = mapElement.transform.localScale.x;
                    elementScaleY = mapElement.transform.localScale.y;
                    
                    mapElement.Init(map[i, j], new MapVector2(i, j));
                    mapElement.name = elementSprite.name;
                    _mapElements.Add(mapElement);
                    
                    TrySetPlayerPositon(mapElement.Index, mapElement.transform);
                    
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

    public Transform GetPlayerTransform()
    {
        if (_playerTransform == null)
            throw new ArgumentNullException(nameof(_playerTransform));
        
        return _playerTransform;
    }

    private void TrySetPlayerPositon(int index, Transform elementTransform)
    {
        if (index == _playerIndex)
            _playerTransform = elementTransform;
    }

    private void ChangeMapElement(MapElement element)
    {
        element.ChangeIndex();
        element.GetComponent<SpriteRenderer>().sprite = _mapElementsSprites[element.Index];
    }
}

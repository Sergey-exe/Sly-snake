using System.Collections;
using System.Collections.Generic;
using _Sources.Input;
using _Sources.Model;
using _Sources.Presenter;
using UnityEngine;

public class MapRoot : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MapPainter _mapPainter;
    [SerializeField] private MapSettings _mapSettings;
    [SerializeField] private PlayerMoveAnimator _playerMoveAnimator;
    
    
    private void Start()
    {
        MapData mapData = new MapData(_mapSettings.Map.GetCells());
        _mapPainter.Init(_mapSettings.EmptyElementPrefab, _playerMoveAnimator, mapData.GetPlayerIndex, _mapSettings.MapElementsPrefabs);
        MapPresenter presenter = new MapPresenter(mapData, _mapPainter);
        _playerMoveAnimator.Init(_mapPainter.GetPlayerTransform());
        _inputReader.Init(presenter);
    }
}

using System.Collections;
using System.Collections.Generic;
using _Sources.Model;
using _Sources.Presenter;
using _Sources.View;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    private LevelTimeCounter _levelTimeCounter;
    private MapSettings _mapSettings;
    private MapPainterInUi _mapPainterInUi;
    private MapPainter _mapPainter;
    private MapData _mapData;
    private MapPresenter _mapPresenter;
    private PlayerMover _playerMover;

    private bool _isInit;
    private bool _isActivate;
    
    public void Init(LevelTimeCounter levelTimeCounter, MapSettings mapSettings, MapPainterInUi mapPainterInUi, MapPainter mapPainter
    , MapData mapData, MapPresenter mapPresenter, PlayerMover playerMover)
    {
        _levelTimeCounter = levelTimeCounter ?? throw new System.ArgumentNullException(nameof(levelTimeCounter));
        _mapSettings = mapSettings ?? throw new System.ArgumentNullException(nameof(mapSettings));
        _mapPainterInUi = mapPainterInUi ?? throw new System.ArgumentNullException(nameof(mapPainterInUi));
        _mapPainter = mapPainter ?? throw new System.ArgumentNullException(nameof(mapPainter));
        _mapData = mapData ?? throw new System.ArgumentNullException(nameof(mapData));
        _mapPresenter = mapPresenter ?? throw new System.ArgumentNullException(nameof(mapPresenter));
        _playerMover = playerMover ?? throw new System.ArgumentNullException(nameof(playerMover));
        
        _isInit = true;
    }
    
    public void Activate()
    {
        if (_isInit)
            _isActivate = true;
    }

    public void BuildLevel()
    {
        if(_isActivate == false)
            return;
        
        _mapSettings.SetLevelIndexes(0, 0); // Здесь должна будет быть загрузка выбранного уровня, сейчас загружается всегда первый
        _mapPainterInUi.SetSprites(_mapSettings.GetMapElementsSprites());
        _mapPainter.SetEmptyElementPrefab(_mapSettings.EmptyElementPrefab);
        _mapPainter.SetPlayerIndex(_mapData.GetPlayerIndex);
        _mapPainter.SetSprites( _mapSettings.GetMapElementsSprites());
        _mapData.SetMap(_mapSettings.GetMap());
        _mapPresenter.CreateNewMap();
        _playerMover.CreatePlayer(_mapPainter.GetStartTransform());
        
        // _levelTimeCounter.StartCounting();
        // _mapSettings.SetLevelIndexes(0, 0); // Здесь должна будет быть загрузка выбранного уровня, сейчас загружается всегда первый
        // _mapPainterInUi.SetSprites(_mapSettings.GetMapElementsSprites());
        // _mapPainter.SetEmptyElementPrefab(_mapSettings.EmptyElementPrefab);
        // _mapPainter.SetPlayerIndex(_mapData.GetPlayerIndex);
        // _mapPainter.SetSprites( _mapSettings.GetMapElementsSprites());
        // _mapData.SetMap(_mapSettings.GetMap());
        // _mapPresenter.CreateNewMap();
        // _playerMover.CreatePlayer(_mapPainter.GetStartTransform());
    }
}

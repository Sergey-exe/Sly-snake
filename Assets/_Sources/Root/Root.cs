using _Sources.Input;
using _Sources.Model;
using _Sources.Presenter;
using _Sources.View;
using UnityEngine;

namespace _Sources.Root
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private SwipeReader _swipeReader;
        [SerializeField] private MapPainter _mapPainter;
        [SerializeField] private MapPainterInUi _mapPainterInUi;
        [SerializeField] private MapSettings _mapSettings;
        [SerializeField] private MapUiSettings _mapSettingsInUi;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private GameModeButtonsListener _gameModeButtonsListener;
        [SerializeField] private LevelTimeCounter _levelTimeCounter;
        [SerializeField] private LevelTimeViewer _levelTimeViewer;
        [SerializeField] private BackgroundSetter _backgroundSetter;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private LevelBuilder _levelBuilder;
    
        private void Start()
        {
            MapData mapData = new MapData();
            EndGameViewer endGameViewer = new EndGameViewer();
            MapPresenter mapPresenter = new MapPresenter(mapData, _mapPainter, endGameViewer, _levelTimeCounter, _mapSettings, _backgroundSetter);
            
            endGameViewer.Init(_mapPainterInUi, _mapSettingsInUi.FailGameWindow, _mapSettingsInUi.WinGameWindow);
            _mapPainterInUi.Init(_mapSettingsInUi.Step, _mapSettingsInUi.Offset, _mapSettingsInUi.EmptyElementPrefabInUi);
            _cameraShaker.Init();
            _playerMover.Init(_cameraShaker);
            _inputReader.Init(mapPresenter);
            _swipeReader.Init(_inputReader);
            _gameModeButtonsListener.Init(mapPresenter);
            _levelTimeCounter.Init(_levelTimeViewer);
            _mapPainter.Init(_playerMover);
            _levelBuilder.Init(_levelTimeCounter, _mapSettings, _mapPainterInUi, _mapPainter, mapData, mapPresenter, _playerMover);
            
            /*
             _levelTimeViewer.Activate();
            _levelTimeCounter.Activate();
            _levelTimeViewer.Activate();
            _mapSettings.SetLevelIndexes(0, 0); // Здесь должна будет быть загрузка выбранного уровня, сейчас загружается всегда первый
            _mapPainterInUi.SetSprites(_mapSettings.GetMapElementsSprites());
            _mapPainterInUi.Activate();
            _mapPainter.SetEmptyElementPrefab(_mapSettings.EmptyElementPrefab);
            _mapPainter.SetPlayerIndex(mapData.GetPlayerIndex);
            _mapPainter.SetSprites( _mapSettings.GetMapElementsSprites());
            mapData.SetMap(_mapSettings.GetMap());
            mapPresenter.CreateNewMap();
            _mapPainter.Activate();
            _cameraShaker.Activate();
            _playerMover.CreatePlayer(_mapPainter.GetStartTransform());
            _playerMover.Activate();
            _gameModeButtonsListener.Activate();
            _inputReader.Activate();
            _swipeReader.Activate();
            */
            
            
            _levelTimeViewer.Activate();
            _levelTimeCounter.Activate();
            _mapPainterInUi.Activate();
            _cameraShaker.Activate();
            _gameModeButtonsListener.Activate();
            _inputReader.Activate();
            _swipeReader.Activate();
            _levelBuilder.Activate();
            _playerMover.Activate();
            _mapPainter.Activate();
            
            _levelBuilder.BuildLevel();
        }
    }
}

using _Sources.Input;
using _Sources.Model;
using _Sources.Presenter;
using _Sources.View;
using UnityEngine;

namespace _Sources.Root
{
    public class MapRoot : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private MapPainter _mapPainter;
        [SerializeField] private MapPainterInUi _mapPainterInUi;
        [SerializeField] private MapSettings _mapSettings;
        [SerializeField] private MapUiSettings _mapSettingsInUi;
        [SerializeField] private PlayerMover _playerMover;
    
    
        private void Start()
        {
            MapData mapData = new MapData(_mapSettings.Map.GetCells());
            _mapPainter.Init(_mapSettings.EmptyElementPrefab, _playerMover, mapData.GetPlayerIndex, _mapSettings.MapElementsSprites);
            EndGameViewer endGameViewer = new EndGameViewer();
            endGameViewer.Init(_mapPainterInUi, _mapSettingsInUi.FailGameWindow, _mapSettingsInUi.WinGameWindow);
            _mapPainterInUi.Init(_mapSettingsInUi.EmptyElementPrefabInUi, _mapSettings.MapElementsSprites);
            _mapPainterInUi.Activate();
            MapPresenter presenter = new MapPresenter(mapData, _mapPainter, endGameViewer);
            _playerMover.Init(_mapPainter.GetStartTransform());
            _inputReader.Init(presenter);
            
            _mapPainter.Activate();
            _playerMover.Activate();
            _inputReader.Activate();
        }
    }
}

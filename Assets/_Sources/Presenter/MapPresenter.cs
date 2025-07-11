using System;
using System.Collections.Generic;
using _Sources.Model;
using _Sources.View;
using UnityEngine;

namespace _Sources.Presenter
{
    public class MapPresenter
    {
        private MapPainter _mapPainter;
        private MapData _mapData;
        private EndGameViewer _endGameViewer;
        private LevelTimeCounter _levelTimeCounter;
        private MapSettings _mapSettings;
        private BackgroundSetter _backgroundSetter;

        private bool _isRun = true;

        public MapPresenter(MapData mapData, MapPainter mapPainter, EndGameViewer endGameViewer, LevelTimeCounter levelTimeCounter, MapSettings mapSettings, BackgroundSetter backgroundSetter)
        {
            _mapPainter = mapPainter ?? throw new ArgumentNullException(nameof(mapPainter));
            _mapData = mapData ?? throw new ArgumentNullException(nameof(mapData));
            _endGameViewer = endGameViewer ?? throw new ArgumentNullException(nameof(endGameViewer));
            _levelTimeCounter = levelTimeCounter ?? throw new ArgumentNullException(nameof(levelTimeCounter));
            _mapSettings = mapSettings ?? throw new ArgumentNullException(nameof(mapSettings));
            _backgroundSetter = backgroundSetter ?? throw new ArgumentNullException(nameof(backgroundSetter));
        }

        public void CreateNewMap()
        {
            _backgroundSetter.SetColor(_mapSettings.GetBackgroundColor());
            DrawMap();
            _endGameViewer.CreateFailUi(_mapData.GetMap());
            _levelTimeCounter.StartCounting();
        }

        public void TryPlayerMove(PositionInMap positionInMap)
        {
            if (!_isRun)
                return;
            
            if(_mapPainter.MapChanging)
                return;
            
            List<PositionInMap> сhangeableElementsPositions =
                _mapData.TrySetNewPlayerPosition(positionInMap, out bool? hasFreeSpace, out bool areVictoryConditionsMet);
            
            _mapPainter.ChangeMap(сhangeableElementsPositions, _mapData.GetMap());
            
            if(hasFreeSpace == true & areVictoryConditionsMet == true)
            {
                _endGameViewer.ShoweWin();
                _levelTimeCounter.StopCounting();
                Debug.Log("Win!");
            }
            else if (hasFreeSpace == false || areVictoryConditionsMet == false)
            {
                _endGameViewer.ShoweFaile(_mapData.GetMap());
                _levelTimeCounter.StopCounting();
                Debug.Log("Lose!");
            }
        }

        public void DrawMap()
        {
            _mapPainter.CreateMap(_mapData.GetMap());
        }

        public void Revert()
        {
            _isRun = true;
            _mapData.Revert();
            _mapPainter.RevertMap(_mapData.GetMap());
            _backgroundSetter.SetColor(_mapSettings.GetBackgroundColor());
            _endGameViewer.HideEndWindows();
            _levelTimeCounter.Revert();
            _levelTimeCounter.StartCounting();
            Debug.Log("Revert");
        }

        public void NextLevel()
        {
            if (_mapSettings.TrySetNextLevel())
            {
                _mapData.SetMap(_mapSettings.GetMap());
                Revert();
            }
            
            Debug.Log("Переход небыл вполнен");
        }

        public void Stop()
        {
            _levelTimeCounter.StopCounting();
            _isRun = false;
        }

        public void Run()
        {
            _levelTimeCounter.StartCounting();
            _isRun = true;
        }
    }
}
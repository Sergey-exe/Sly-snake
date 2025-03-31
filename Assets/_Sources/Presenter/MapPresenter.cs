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

        public MapPresenter(MapData mapData, MapPainter mapPainter, EndGameViewer endGameViewer)
        {
            _mapPainter = mapPainter ?? throw new ArgumentNullException(nameof(mapPainter));
            _mapData = mapData ?? throw new ArgumentNullException(nameof(mapData));
            _endGameViewer = endGameViewer ?? throw new ArgumentNullException(nameof(endGameViewer));

            DrawMap();
            _endGameViewer.CreateFailUi(mapData.GetMap());
        }

        public void TryPlayerMove(PositionInMap positionInMap)
        {
            if(_mapPainter.MapChanging)
                return;
            
            List<PositionInMap> сhangeableElementsPositions =
                _mapData.TrySetNewPlayerPosition(positionInMap, out bool? hasFreeSpace, out bool areVictoryConditionsMet);
            
            _mapPainter.ChangeMap(сhangeableElementsPositions, _mapData.GetMap());
            
            if(hasFreeSpace == true & areVictoryConditionsMet == true)
            {
                Debug.Log("Win!");
            }
            else if (hasFreeSpace == false || areVictoryConditionsMet == false)
            {
                _endGameViewer.ShoweFaile(_mapData.GetMap());
                
                _mapData.Revert();
                _mapPainter.RevertMap(_mapData.GetMap());
                Debug.Log("Lose!");
            }
        }

        public void DrawMap()
        {
            _mapPainter.CreateMap(_mapData.GetMap());
        }
    }
}
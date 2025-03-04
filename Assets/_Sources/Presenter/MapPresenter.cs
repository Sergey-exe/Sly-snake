using System;
using _Sources.Model;

namespace _Sources.Presenter
{
    public class MapPresenter
    {
        private MapPainter _mapPainter;
        private MapData _mapData;

        public MapPresenter(MapData mapData, MapPainter mapPainter)
        {
            _mapPainter = mapPainter ?? throw new ArgumentNullException(nameof(mapPainter));
            _mapData = mapData ?? throw new ArgumentNullException(nameof(mapData));

            DrawMap();
        }

        public void TryPlayerMove(MapVector2 mapVector2)
        {
            _mapPainter.ChangeMap(_mapData.TrySetNewPlayerPosition(mapVector2), _mapData.GetMap());
        }

        public void DrawMap()
        {
            _mapPainter.CreateMap(_mapData.GetMap());
        }
    }
}
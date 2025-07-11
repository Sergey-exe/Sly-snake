using System;
using _Sources.View;
using UnityEngine;

public class EndGameViewer
{
    private MapPainterInUi _mapPainterInUi;
    private GameObject _winWindow;
    private GameObject _failWindow;
    
    private bool _isInit;

    public void Init(MapPainterInUi mapPainterInUi, GameObject failWindow, GameObject winWindow)
    {
        _mapPainterInUi = mapPainterInUi ?? throw new ArgumentNullException(nameof(mapPainterInUi));
        _failWindow = failWindow ?? throw new ArgumentNullException(nameof(failWindow));
        _winWindow = winWindow ?? throw new ArgumentNullException(nameof(winWindow));
        
        _isInit = true;
    }

    public void ShoweFaile(int[,] map)
    {
        if(_isInit == false)
            return;
        
        Debug.Log(nameof(ShoweFaile));
        _failWindow.SetActive(true);
        _mapPainterInUi.ChangeMap(map);
    }
    
    public void ShoweWin()
    {
        if(_isInit == false)
            return;
        
        _winWindow.SetActive(true);
    }

    public void HideEndWindows()
    {
        if(_isInit == false)
            return;
        
        _winWindow.SetActive(false);
        _failWindow.SetActive(false);
    }

    public void CreateFailUi(int[,] map)
    {
        if(_isInit == false)
            return;
        
        _mapPainterInUi.CreateMap(map);
    }
}

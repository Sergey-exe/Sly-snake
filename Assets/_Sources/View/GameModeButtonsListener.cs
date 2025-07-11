using System;
using _Sources.Presenter;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButtonsListener : MonoBehaviour
{
    [SerializeField] private Button[] _restartButtons;
    [SerializeField] private Button[] _pauseButtons;
    [SerializeField] private Button[] _resumeButtons;
    [SerializeField] private Button[] _nextLevelButtons;
    
    private MapPresenter _mapPresenter;
    private bool _isInit;
    private bool _isActive;

    private void OnDisable()
    {
        Unsubscribe();
        _isActive = false;
    }

    public void Init(MapPresenter mapPresenter)
    {
        _mapPresenter = mapPresenter ?? throw new ArgumentNullException(nameof(mapPresenter)); 
        
        _isInit = true;
    }

    public void Activate()
    {
        if(_isInit == false)
            return;

        Subscribe();
        _isActive = true;
    }

    private void Subscribe()
    {
        foreach (var button in _restartButtons)
            button.onClick.AddListener(_mapPresenter.Revert);

        foreach (var button in _pauseButtons)
            button.onClick.AddListener(_mapPresenter.Stop);
        
        foreach (var button in _nextLevelButtons)
            button.onClick.AddListener(_mapPresenter.NextLevel);
    }

    private void Unsubscribe()
    {
        foreach (var button in _restartButtons)
            button.onClick.RemoveListener(_mapPresenter.Revert);
        
        foreach (var button in _pauseButtons)
            button.onClick.RemoveListener(_mapPresenter.Stop);
        
        foreach (var button in _nextLevelButtons)
            button.onClick.RemoveListener(_mapPresenter.NextLevel);
    }
}

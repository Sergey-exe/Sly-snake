using System;
using _Sources.Input;
using _Sources.Model;
using UnityEngine;
using Lean.Touch;

public class SwipeReader : MonoBehaviour
{
    private const float MinSwipeDistance = 50;

    private InputReader _inputReader;
    private int _step = 1;
    private bool _isInit;
    private bool _isActive;

    private void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= HandleSwipe;
        _isActive = false;
    }

    public void Init(InputReader inputReader)
    {
        _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
        _isInit = true;
    }

    public void Activate()
    {
        if (!_isInit)
            return;
        
        if(!Application.isMobilePlatform)
            return;
        
        LeanTouch.OnFingerSwipe += HandleSwipe;
        _isActive = true;
    }
    
    private void HandleSwipe(LeanFinger finger)
    {
        if (!_isActive)
            return;
        
        if (finger.SwipeScreenDelta.magnitude >= MinSwipeDistance)
        {
            Vector2 swipeDirection = finger.SwipeScreenDelta.normalized;
            ProcessSwipe(swipeDirection);
        }
    }

    private void ProcessSwipe(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                _inputReader.EnterDirection(new PositionInMap(0, _step));
            else
                _inputReader.EnterDirection(new PositionInMap(0, -_step));
        }
        else
        {
            if (direction.y > 0)
                _inputReader.EnterDirection(new PositionInMap(-_step, 0));
            else
                _inputReader.EnterDirection(new PositionInMap(_step, 0));
        }
    }
}

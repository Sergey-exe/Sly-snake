using System;
using System.Collections;
using System.Collections.Generic;
using _Sources.View;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{ 
    [SerializeField] private float _speed;
    [SerializeField] private Player _playerPrfab;
    
    private Transform _playerTransform;
    private Coroutine _moveCoroutine;
    private List<MapElement> _waypoints;
    private CameraShaker _cameraShaker;
    private bool _hasPlayer;
    private bool _isInit;
    private bool _isActive;
    
    public event Action<MapElement> InMapElement;
    
    public event Action Finished;
    
    public void Activate()
    {
        if(_isInit == false)
            return;
            
        _isActive = true;
    }

    public void Init(CameraShaker cameraShaker)
    {
        _cameraShaker = cameraShaker ?? throw new ArgumentNullException(nameof(cameraShaker));
       
        _isInit = true;
    }

    public void CreatePlayer(Transform playerTransform)
    {
        Player player = Instantiate(_playerPrfab, playerTransform.position, playerTransform.rotation);
        
        _playerTransform = player.transform;
        
        _hasPlayer = true;
    }

    public void SetPosition(Transform playerTransform)
    {
        if (_hasPlayer == false)
            throw new ArgumentNullException("Player не создан");
        
        _playerTransform.position = playerTransform.position;
    }

    public void SetMovementPosition(List<MapElement> waypoints)
    {
        _waypoints = waypoints ?? throw new ArgumentNullException(nameof(waypoints));
    }
    
    public void StartMove()
    {
        if (_isActive == false)
            return;
        
        if(_moveCoroutine != null)
            return;
        
        if (_waypoints == null)
            return;
        
        _moveCoroutine = StartCoroutine(Move());
    }

    public void StopMove()
    {
        if (_moveCoroutine == null)
            return;
        
        StopCoroutine(_moveCoroutine);
        _moveCoroutine = null;
    }
    
    private IEnumerator Move()
    {
        if (_hasPlayer == false)
            throw new ArgumentNullException("Player не создан");
        
        float epsilon = 0.01f;
        
        while (_waypoints.Count != 0)
        {
            if (Vector2.Distance(_playerTransform.position, _waypoints[0].transform.position) >= epsilon)
            {
                _playerTransform.position = Vector2.MoveTowards(_playerTransform.position, _waypoints[0].transform.position, _speed * Time.deltaTime);
            }
            else
            {
                InMapElement?.Invoke(_waypoints[0]);
                _waypoints.RemoveAt(0);
            }

            yield return null;
        }

        _cameraShaker.Shake();
        _moveCoroutine = null;
        Finished?.Invoke();
        yield return null;
    }
}

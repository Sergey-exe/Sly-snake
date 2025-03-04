using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMoveAnimator : MonoBehaviour
{ 
    [SerializeField] private float _speed;
    [SerializeField] private Player _playerPrfab;
    
    private Transform _playerTransform;
    private Coroutine _moveCoroutine;
    private List<MapElement> _waypoints;
    private bool _isInit;
    
    public event Action<MapElement> Finished;

    public void Init(Transform playerTransform)
    {
        Player player = Instantiate(_playerPrfab, playerTransform.position, playerTransform.rotation);
        
        _playerTransform = player.transform;
        _isInit = true;
    }

    public void SetMovementPosition(List<MapElement> waypoints)
    {
        _waypoints = waypoints ?? throw new ArgumentNullException(nameof(waypoints));
    }
    
    public void StartMove()
    {
        if (_isInit == false)
            return;
        
        if(_moveCoroutine != null)
            return;
        
        if (_waypoints == null)
            return;
        
        _moveCoroutine = StartCoroutine(Move());
    }
    
    private IEnumerator Move()
    {
        float epsilon = 0.001f;
        bool atOnePoint = false;
        
        while (atOnePoint == false)
        {
            if (_waypoints == null || _waypoints.Count == 0)
            {
                _moveCoroutine = null;
                yield break;
            }
            
            if (Vector2.Distance(_playerTransform.position, _waypoints[0].transform.position) >= epsilon)
            {
                _playerTransform.position = Vector2.MoveTowards(_playerTransform.position, _waypoints[0].transform.position, _speed * Time.deltaTime);
            }
            else
            {
                Finished?.Invoke(_waypoints[0]);
                _waypoints.RemoveAt(0);
            }

            yield return null;
        }
    }
}

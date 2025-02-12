using System;
using _Sources.Input;
using _Sources.Model;
using UnityEngine;

namespace _Sources.Presenter
{
    public class PlayerPresenter : MonoBehaviour
    {
        private float _speed;
        private float _rayLength;
        private PlayerMover _playerMover;
        private InputReader _inputReader;
        private bool _isInit;

        
        private void Update()
        {
            if (_isInit == false)
                return;
            
            if (_inputReader.DownButtonUp())
                TryMove(Vector2.up);
            else if (_inputReader.DownButtonDown())
                TryMove(Vector2.down);
            else if (_inputReader.DownButtonLeft())
                TryMove(Vector2.left);
            else if (_inputReader.DownButtonRight())
                TryMove(Vector2.right);
        }

        public void Init(float speed, float rayLenht, PlayerMover playerMover, InputReader inputReader)
        {
            _speed = speed;
            _rayLength = rayLenht;
            _playerMover = playerMover ?? throw new ArgumentNullException(nameof(playerMover));
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _isInit = true;
        }

        private void TryMove(Vector2 direction)
        {
            _playerMover.TryMove(direction, _rayLength, _speed);
        }
    }
}
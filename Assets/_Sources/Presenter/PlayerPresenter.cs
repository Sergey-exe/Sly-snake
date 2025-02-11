using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rayLength;

    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private InputReader _inputReader;

    private void Update()
    {
        if (_inputReader.DownButtonUp())
            TryMove(Vector2.up);
        else if (_inputReader.DownButtonDown())
            TryMove(Vector2.down);
        else if (_inputReader.DownButtonLeft())
            TryMove(Vector2.left);
        else if (_inputReader.DownButtonRight())
            TryMove(Vector2.right);
    }

    private void TryMove(Vector2 direction)
    {
        _playerMover.TryMove(direction, _rayLength, _speed);
    }
}

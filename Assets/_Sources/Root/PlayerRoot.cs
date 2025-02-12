using _Sources.Input;
using _Sources.Model;
using _Sources.Presenter;
using UnityEngine;

namespace _Sources.Root
{
    public class PlayerRoot : Root
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rayLength;
        [SerializeField] private LayerMask _woolLayerMask;
        [SerializeField] private Player _player;
        [SerializeField] private InputReader _inputReader;
    
        public override void Init()
        {
            PlayerPresenter playerPresenter = _player.gameObject.AddComponent<PlayerPresenter>();
            PlayerMover playerMover = _player.gameObject.AddComponent<PlayerMover>();
            playerMover.Init(_woolLayerMask);
            playerPresenter.Init(_moveSpeed, _rayLength, playerMover, _inputReader);
        }
    }
}
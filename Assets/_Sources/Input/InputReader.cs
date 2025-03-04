using System.Linq;
using _Sources.Model;
using _Sources.Presenter;
using UnityEngine;

namespace _Sources.Input
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private KeyCode[] _keysUp;
        [SerializeField] private KeyCode[] _keysDown;
        [SerializeField] private KeyCode[] _keysRight;
        [SerializeField] private KeyCode[] _keysLeft;

        private MapPresenter _mapPresenter;
        private bool _isInit;

        private void Update()
        {
            if(_isInit == false)
                return;
            
            if(DownButtonUp())
                _mapPresenter.TryPlayerMove(new MapVector2(-1, 0));
            else if(DownButtonDown())
                _mapPresenter.TryPlayerMove(new MapVector2(1, 0));
            else if(DownButtonRight())
                _mapPresenter.TryPlayerMove(new MapVector2(0, 1));
            else if(DownButtonLeft())
                _mapPresenter.TryPlayerMove(new MapVector2(0, -1));
        }

        public void Init(MapPresenter mapPresenter)
        {
            _mapPresenter = mapPresenter ?? throw new System.ArgumentNullException(nameof(mapPresenter));
            
            _isInit = true;
        }

        public bool DownButtonUp()
        {
            return DownButton(_keysUp);
        }

        public bool DownButtonDown()
        {
            return DownButton(_keysDown);
        }

        public bool DownButtonRight()
        {
            return DownButton(_keysRight);
        }

        public bool DownButtonLeft()
        {
            return DownButton(_keysLeft);
        }

        private bool DownButton(KeyCode[] keyCodes)
        {
            return keyCodes.Any(keyCode => UnityEngine.Input.GetKeyDown(keyCode));
        }
    }
}

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
        private int _step = 1;
        private bool _isMobile;
        private bool _isInit;
        private bool _isActive;

        private void Update()
        {
            if(_isActive == false)
                return;

            if(DownButtonUp())
                _mapPresenter.TryPlayerMove(new PositionInMap(-_step, 0));
            else if(DownButtonDown())
                _mapPresenter.TryPlayerMove(new PositionInMap(_step, 0));
            else if(DownButtonRight())
                _mapPresenter.TryPlayerMove(new PositionInMap(0, _step));
            else if(DownButtonLeft())
                _mapPresenter.TryPlayerMove(new PositionInMap(0, -_step));
        }

        public void Activate()
        {
            if(_isInit == false)
                return;
            
            _isActive = true;
        }

        public void Init(MapPresenter mapPresenter)
        {
            _mapPresenter = mapPresenter ?? throw new System.ArgumentNullException(nameof(mapPresenter));
            _isMobile = Application.isMobilePlatform;
            
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

        public void EnterDirection(PositionInMap positionInMap)
        {
            _mapPresenter.TryPlayerMove(positionInMap);
        }

        private bool DownButton(KeyCode[] keyCodes)
        {
            return keyCodes.Any(keyCode => UnityEngine.Input.GetKeyDown(keyCode));
        }
    }
}

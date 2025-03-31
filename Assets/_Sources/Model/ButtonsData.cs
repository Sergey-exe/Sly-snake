using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Sources.Model
{
    public class ButtonsData
    {
        private List<ButtonInMap> _buttons;
        private List<ButtonInMap> _downButtons;

        public ButtonsData()
        {
            _buttons = new List<ButtonInMap>();
            _downButtons = new List<ButtonInMap>();
        }

        public void AddButtons(List<ButtonInMap> buttons)
        {
            if (buttons == null)
                throw new ArgumentNullException(nameof(buttons));
            
            _buttons.Clear();
            _buttons.AddRange(buttons);
        }

        public void TryDownButton(PositionInMap buttonPosition)
        {
            if (_buttons == null)
                throw new ArgumentNullException(nameof(buttonPosition));

            foreach (var button in _buttons)
            {
                if (button.Position.X == buttonPosition.X & button.Position.Y == buttonPosition.Y)
                {
                    if(TryShowDownButton(button.Position) == false)
                        _downButtons.Add(button);

                    return;
                }
            }
        }

        public bool AreButtonsPressedInOrder()
        {
            if (_downButtons == null)
                throw new ArgumentNullException(nameof(_downButtons));
            
            int order = 0;

            for (int i = 0; i < _downButtons.Count; i++)
            {
                if (_downButtons[i].Order - order != 1)
                    return false;
                
                order = _downButtons[i].Order;
            }
            
            return true;
        }

        public void Revert()
        {
            _downButtons.Clear();
        }

        private bool TryShowDownButton(PositionInMap buttonPosition)
        {
            foreach (var button in _downButtons)
            {
                if (button.Position.X == buttonPosition.X & button.Position.Y == buttonPosition.Y)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
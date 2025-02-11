using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keysUp;
    [SerializeField] private KeyCode[] _keysDown;
    [SerializeField] private KeyCode[] _keysRight;
    [SerializeField] private KeyCode[] _keysLeft;

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
        foreach (KeyCode keyCode in keyCodes)
            if (Input.GetKeyDown(keyCode))
                return true;

        return false;
    }
}

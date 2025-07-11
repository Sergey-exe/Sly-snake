using UnityEngine;

public class BackgroundSetter : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public void SetColor(Color color)
    {
        _camera.backgroundColor = color;
    }
}

using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeAmplitude; 
    [SerializeField] private float _shakeFrequency;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    private CinemachineBasicMultiChannelPerlin _noiseComponent;
    private bool _isShaking;
    private float _currentShakeDuration;

    public bool IsInit { get; private set; }
    
    public bool IsActivate { get; private set; }
    
    public void Init()
    {
        if (_virtualCamera == null)
        {
            Debug.LogError("Virtual Camera не назначена.");
            return;
        }

        _noiseComponent = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        if (_noiseComponent == null)
        {
            Debug.LogError("Компонент шума не найден на виртуальной камере.");
            return;
        }

        IsInit = true;
    }
    
    public void Activate()
    {
        if (IsInit)
            IsActivate = true;
        else
            Debug.LogWarning("Камера не инициализирована. Вызовите Init() перед Activate().");
    }

    public void Shake()
    {
        if (IsActivate && !_isShaking)
        {
            _currentShakeDuration = _shakeDuration;
            _isShaking = true;
            StartCoroutine(ShakeCoroutine());
        }
        else if (!IsActivate)
        {
            Debug.LogWarning("Камера не активирована. Вызовите Activate() перед вызовом Shake().");
        }
    }

    private System.Collections.IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _currentShakeDuration)
        {
            float damper = 1f - (elapsedTime / _currentShakeDuration);
            float amplitude = _shakeAmplitude * damper;

            SetNoiseParameters(amplitude, _shakeFrequency);

            yield return null;
            
            elapsedTime += Time.deltaTime;
        }
        
        SetNoiseParameters(0f, 0f);
        _isShaking = false;
    }
    
    private void SetNoiseParameters(float amplitude, float frequency)
    {
        if (_noiseComponent != null)
        {
            _noiseComponent.m_AmplitudeGain = amplitude;
            _noiseComponent.m_FrequencyGain = frequency;
        }
    }
}
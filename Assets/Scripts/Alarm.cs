using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    private const string IsTurnedOn = "IsTurnedOn";
    
    [SerializeField] private float _volumeChangeSpeed = 0.5f;
    
    private AudioSource _audioSource;
    private Animator _animator;
    private float _maxVolume = 1;
    private float _minVolume = 0;
    private bool _isTurnedOn = false;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _audioSource.volume = _minVolume;
        _audioSource.Play();
    }

    public void TurnOn()
    {
        _isTurnedOn = true;
        UpdateAlarmState();
    }

    public void TurnOff()
    {
        _isTurnedOn = false;
        UpdateAlarmState();
    }
    
    private void UpdateAlarmState()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        float targetVolume = _isTurnedOn ? _maxVolume : _minVolume;
            
        _volumeCoroutine = StartCoroutine(AdjustVolume(targetVolume));
        _animator.SetBool(IsTurnedOn, _isTurnedOn);
    }

    private IEnumerator AdjustVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            float volumeChangeSpeed = _volumeChangeSpeed * Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeChangeSpeed);
            yield return null;
        }
    }
}
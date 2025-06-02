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
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _audioSource.volume = _minVolume;
    }

    public void SetAlarm(bool isAlarmTurnedOn)
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(AdjustVolume(isAlarmTurnedOn));
        _animator.SetBool(IsTurnedOn, isAlarmTurnedOn);

        if (isAlarmTurnedOn)
            _audioSource.Play();
    }

    private IEnumerator AdjustVolume(bool isAlarmTurnedOn)
    {
        float targetVolume = isAlarmTurnedOn ? _maxVolume : _minVolume;
        
        while (_audioSource.volume != targetVolume)
        {
            float volumeChangeSpeed = _volumeChangeSpeed * Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeChangeSpeed);
            yield return null;
        }

        if (isAlarmTurnedOn == false)
            _audioSource.Stop();
    }
}
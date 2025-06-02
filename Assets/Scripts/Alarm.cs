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
        StopCurrentCoroutine();
        _isTurnedOn = true;
        _volumeCoroutine = StartCoroutine(AdjustVolume());
        _animator.SetBool(IsTurnedOn, _isTurnedOn);
    }

    public void TurnOff()
    {
        StopCurrentCoroutine();
        _isTurnedOn = false;
        _volumeCoroutine = StartCoroutine(AdjustVolume());
        _animator.SetBool(IsTurnedOn, _isTurnedOn);
    }

    private void StopCurrentCoroutine()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);
    }

    private IEnumerator AdjustVolume()
    {
        float targetVolume = _isTurnedOn ? _maxVolume : _minVolume;
        
        while (_audioSource.volume != targetVolume)
        {
            float volumeChangeSpeed = _volumeChangeSpeed * Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeChangeSpeed);
            yield return null;
        }
    }
}
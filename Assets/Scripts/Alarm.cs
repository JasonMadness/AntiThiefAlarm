using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    private const string IsOn = "IsOn";
    
    [SerializeField] private float _volumeIncreaseSpeed;
    [SerializeField] private float _volumeDecreaseSpeed;

    private AudioSource _audioSource;
    private Animator _animator;
    private float _maxVolume = 1;
    private float _minVolume = 0;
    private bool _isRobberingInProgress = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void Switch()
    {
        _isRobberingInProgress = !_isRobberingInProgress;

        if (_isRobberingInProgress)
            StartCoroutine(TurnOn());
        else 
            StartCoroutine(TurnOff());
    }

    private IEnumerator TurnOn()
    {
        _audioSource.Play();
        _animator.SetBool(IsOn, true);

        while (_audioSource.volume < _maxVolume && _isRobberingInProgress)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _volumeIncreaseSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator TurnOff()
    {
        while (_audioSource.volume > _minVolume && _isRobberingInProgress == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _volumeDecreaseSpeed * Time.deltaTime);
            yield return null;
        }

        _audioSource.Stop();
        _animator.SetBool(IsOn, false);
    }
}

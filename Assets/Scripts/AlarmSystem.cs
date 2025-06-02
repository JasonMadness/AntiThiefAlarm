using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private ThiefDetector _thiefDetector;
    [SerializeField] private Alarm _alarm;

    private void OnEnable()
    {
        _thiefDetector.ThiefEntered += OnThiefEntered;
        _thiefDetector.ThiefExited += OnThiefExited;
    }

    private void OnDisable()
    {
        _thiefDetector.ThiefEntered -= OnThiefEntered;
        _thiefDetector.ThiefExited -= OnThiefExited;
    }

    private void OnThiefEntered()
    {
        _alarm.TurnOn();
    }

    private void OnThiefExited()
    {
        _alarm.TurnOff();
    }
}
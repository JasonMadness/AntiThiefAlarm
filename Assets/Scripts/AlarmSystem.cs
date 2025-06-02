using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private ThiefDetector thiefDetector;
    [SerializeField] private Alarm _alarm;

    private void OnEnable()
    {
        thiefDetector.ThiefEntered += OnThiefEntered;
        thiefDetector.ThiefExited += OnThiefExited;
    }

    private void OnDisable()
    {
        thiefDetector.ThiefEntered -= OnThiefEntered;
        thiefDetector.ThiefExited -= OnThiefExited;
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
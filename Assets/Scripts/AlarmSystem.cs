using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Alarm _alarm;

    private void OnEnable()
    {
        _door.ThiefEntered += OnThiefEntered;
        _door.ThiefExited += OnThiefExited;
    }

    private void OnDisable()
    {
        _door.ThiefEntered -= OnThiefEntered;
        _door.ThiefExited -= OnThiefExited;
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
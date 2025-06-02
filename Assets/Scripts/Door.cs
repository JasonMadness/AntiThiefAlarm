using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(typeof(Thief), out _))
            ThiefEntered?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(typeof(Thief), out _))
            ThiefExited?.Invoke();
    }
}
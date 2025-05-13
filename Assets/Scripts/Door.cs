using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThiefEntered?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ThiefExited?.Invoke();
    }
}
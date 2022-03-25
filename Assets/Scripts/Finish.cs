using System;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] private UnityEvent _finishGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            _finishGame?.Invoke();
            Active();
        }
    }

    private void Active()
    {
        Time.timeScale = 0;
        Debug.Log("Player Win!");
    }
}

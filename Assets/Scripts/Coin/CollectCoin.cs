using UnityEngine;
using UnityEngine.Events;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] private UnityEvent _coinReceived;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _coinReceived?.Invoke();
            Debug.Log("Player Collect Coin!");
        }
    }
}

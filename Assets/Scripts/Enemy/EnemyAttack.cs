using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private UnityEvent _damageDealt;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            _damageDealt?.Invoke();
    }
}

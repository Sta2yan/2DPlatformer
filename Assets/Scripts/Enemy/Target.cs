using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public Transform Coordinates => transform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
            enemyMovement.StartStayTime();
    }
}

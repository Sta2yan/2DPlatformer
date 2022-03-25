using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage()
    {
        Time.timeScale = 0;
        Debug.Log("Player dead!");
    }
}

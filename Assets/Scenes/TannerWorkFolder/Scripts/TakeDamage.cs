using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public PlayerHealthManager playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            playerHealth.TakeDamage();
        }
    }
}

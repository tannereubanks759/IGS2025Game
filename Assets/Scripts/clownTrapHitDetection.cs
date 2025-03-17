using UnityEngine;

public class clownTrapHitDetection : MonoBehaviour
{
    public PlayerHealthManager playerHealthManagerRef;
    public ClownRideMovement clownRideMovementRef;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(clownRideMovementRef.isActive)
        {
            if (collision.gameObject.layer == 8)
            {
                collision.gameObject.GetComponentInParent<zombieAIV1>().TakeDamage(10);

            }
            /*if (collision.gameObject.layer == 11)
            {
                playerHealthManagerRef.pause.Die();
            }*/
        }
       
    }
}

using UnityEngine;

public class flamethrowerDamage : MonoBehaviour
{
    // The particle collides
    private void OnParticleCollision(GameObject other)
    {
        // The colliding body is in the zombie layer
        if (other.layer == 8)
        {
            // Set the onFire variable to true
            other.GetComponent<zombieAIV1>().onFire = true;
        }
    }
}

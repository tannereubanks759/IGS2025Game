using UnityEngine;

public class ZombieCulling : MonoBehaviour
{
    zombieAIV1 zombieAIV1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zombieAIV1 = GetComponentInParent<zombieAIV1>();
    }

    private void OnBecameInvisible()
    {
        if (zombieAIV1.isZombieDead)
        {
            Destroy(zombieAIV1.gameObject);
        }
    }
}

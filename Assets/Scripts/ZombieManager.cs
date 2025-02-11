using UnityEngine;
using System.Collections;

public class ZombieManager : MonoBehaviour
{
    // Holds the total and current number of zombies
    public int totalZombies;
    [SerializeField] private int maxZombies;

    // a bool to tell the spawners if they can or cannot spawn
    public bool canSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Zombie());
    }

    // checks the total # of zombies and sets the spawn bool
    IEnumerator Zombie()
    {
        if (totalZombies >= maxZombies)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
        }

        yield return new WaitForSeconds(.1f);
    }
}

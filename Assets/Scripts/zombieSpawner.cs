using System;
using UnityEngine;

public class zombieSpawner : MonoBehaviour
{
    // The zombie prefabs
    [SerializeField] GameObject[] zombies;

    // The game time
    private float gameTime;

    private bool playerNear;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    void Update()
    {
    }

    // Time calculations
    void GameTimeManager()
    {
        gameTime += Time.deltaTime;

        if (gameTime > 4 && playerNear)
        {

        }
    }

}

using UnityEngine;

public class LightFlashCopZombie : MonoBehaviour
{
    [SerializeField] private GameObject lightSource;
    [SerializeField] private float flashInterval;
    private float lightTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lightTimer += Time.deltaTime;

        if (lightTimer > flashInterval)
        {
            SwitchLight();
        }
    }

    void SwitchLight()
    {
        lightSource.SetActive(!lightSource.activeSelf);

        lightTimer = 0f;
    }
}

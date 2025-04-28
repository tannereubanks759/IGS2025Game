
using UnityEngine;
using UnityEngine.UI;

public class GrenadeManager : MonoBehaviour
{
    public GameObject grenadePref;
    public float cooldown;
    public GameObject grenadeSpawn;
    bool spawnedGrenade;
    float nextTime;
    public RawImage grenadeIcon;
    public bool canShoot;
    // Update is called once per frame
    private void Start()
    {
        nextTime = Time.time;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && spawnedGrenade == false && canShoot)
        {
            Instantiate(grenadePref, grenadeSpawn.transform.position, grenadeSpawn.transform.rotation);
            spawnedGrenade = true;
            Color newColor = new Color(0, 0, 0, 0);
            grenadeIcon.color = newColor;
            Invoke("reload", cooldown);
        }
        if(spawnedGrenade == true && Time.time > nextTime)
        {
            Color newColor = new Color(0, 0, 0, grenadeIcon.color.a + .1f);
            grenadeIcon.color = newColor;
            nextTime = Time.time + 1f;
        }
    }
    void reload()
    {
        spawnedGrenade = false;
        grenadeIcon.color = Color.white;
    }
}

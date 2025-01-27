using Unity.VisualScripting;
using UnityEngine;

public class GunAnimEvents : MonoBehaviour
{
    public GunScript gun;
    public void shoot()
    {
        gun.Fire();
    }
    public void reload()
    {
        gun.Reload();
    }
}

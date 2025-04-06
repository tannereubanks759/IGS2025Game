using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    public GameObject Truck;
    public Animator Anim;
    public FirstPersonController player;
    private Truck truckScript;
    public AudioSource truckAudio;
    public AudioClip startEngine;
    public AudioClip driveSound;
    public GameObject PlayerUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        truckScript = Truck.GetComponent<Truck>();
    }

    public void PlayCutscene()
    {
        PlayerUI.SetActive(false);
        DisablePlayer();
        Anim.SetTrigger("Start");
    }

    public void StartEngine()
    {
        truckAudio.PlayOneShot(startEngine);
    }

    public void Drive()
    {
        truckAudio.PlayOneShot(driveSound);
    }

    public void DisablePlayer()
    {
        player.gameObject.SetActive(false);
    }

    public void End()
    {
        truckScript.ENDGAME();
    }


}

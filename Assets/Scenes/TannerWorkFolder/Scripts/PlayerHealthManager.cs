using System.Linq;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private Animator anim;
    public PauseMenu pause;
    public float healTime;
    private float nextHeal;
    private int health;

    // Audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] hurtList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.anim = GetComponent<Animator>();
        anim.SetInteger("Health", 5);
    }

    public void Update()
    {
        health = anim.GetInteger("Health");
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }
        if(health != 5 && Time.time > nextHeal)
        {
            anim.SetInteger("Health", 5);
        }

    }
    public void TakeDamage()
    {
        nextHeal = Time.time + healTime;
        anim.SetInteger("Health", health - 1);

        // Play a random audio clip from the list
        var index = Random.Range(0, hurtList.Length - 1);
        PlayHurtAudio(index);
    }

    // Plays audio
    public void PlayHurtAudio(int i)
    {
        Debug.Log("Hurt Audio " + i);
        audioSource.clip = hurtList[i];
        audioSource.Play();
    }
    public void Die()
    {
        pause.Die();
    }
}

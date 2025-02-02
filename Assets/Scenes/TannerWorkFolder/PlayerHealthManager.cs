using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private Animator anim;
    public PauseMenu pause;
    public float healTime;
    private float nextHeal;
    private int health;
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
    }
    public void Die()
    {
        pause.Die();
    }
}

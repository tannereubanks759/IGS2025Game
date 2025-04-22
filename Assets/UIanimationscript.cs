using UnityEngine;

public class UIanimationscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    public void callResetTrigger()
    {
        animator.SetTrigger("reset");
    }
}

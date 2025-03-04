using UnityEngine;

public class KiddieCoaster : MonoBehaviour
{
    public Animator coasterAnimator;

    public GameObject interactUI;

    public miniGameScript miniGameObject;

    public int costPrice;

    public bool paid;

    [SerializeField] int revolutionsCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactUI.SetActive(false);

        paid = false;

        revolutionsCompleted = 0;
    }

    public void resetAnimationBool()
    {
        if (revolutionsCompleted == 3)
        {
            coasterAnimator.SetBool("isOn", false);

            revolutionsCompleted = 0;
        }
        else
        {
            revolutionsCompleted++;
        }
    }

    public void startCoaster()
    {
        if (miniGameObject.tickets >= costPrice && paid == false)
        {
            paid = true;
            coasterAnimator.SetBool("isOn", true);
            miniGameObject.tickets -= costPrice;
            miniGameObject.ticketText.text = miniGameObject.tickets.ToString();
        }
    }
}
